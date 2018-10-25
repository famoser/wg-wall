import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Component, OnInit } from '@angular/core';
import {
  faCheck, faPencilAlt, faPlus, faSave, faUndo, faTrash
} from '@fortawesome/free-solid-svg-icons';

import { TaskTemplate } from '../models/task-template';
import { TaskTemplateService } from '../services/task-template.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  //icons
  public faCheck = faCheck;
  public faTrash = faTrash;
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faUndo = faUndo;
  public faPlus = faPlus;

  //task lists
  public taskTemplates: TaskTemplate[] = [];

  //to disable buttons when appropiate
  public actionsActive: number;

  //edit entries
  public editSource: TaskTemplate;
  public editContainer: TaskTemplate;

  //confirm entry
  public executeSource: TaskTemplate;

  constructor(private taskTemplateService: TaskTemplateService) { }

  ngOnInit() {
    this.taskTemplateService.get().subscribe(taskTemplates => {
      taskTemplates.forEach(tt => {
        tt.expectedRelativeCompletion = this.calculateExpectedRelativeCompletion(tt);
      });
      taskTemplates.sort((a, b) => b.expectedRelativeCompletion - a.expectedRelativeCompletion);

      this.taskTemplates = taskTemplates;

      if (this.taskTemplates.length == 0) {
        this.startAdd();
      }
    });
  }

  private calculateExpectedRelativeCompletion(taskTemplate: TaskTemplate) {
    if (taskTemplate.lastExecutionAt == null) {
      return 1;
    }

    if (taskTemplate.intervalInDays <= 0) {
      return 0;
    }

    //miliseconds of the interval days
    var expectedTicks = taskTemplate.intervalInDays * 60 * 60 * 24 * 1000;
    var passedTicks = new Date().valueOf() - new Date(taskTemplate.lastExecutionAt).valueOf();
    return passedTicks / expectedTicks;
  }

  public startAdd() {
    this.editContainer = new TaskTemplate();
    this.editContainer.reward = 1;
    this.editContainer.intervalInDays = 0;
  }

  public startEdit(source: TaskTemplate) {
    this.editSource = source;
    this.editContainer = new TaskTemplate();
    this.editContainer.name = source.name;
    this.editContainer.reward = source.reward;
    this.editContainer.intervalInDays = source.intervalInDays;
  }

  public add(source: TaskTemplate) {
    //lock
    this.actionsActive++;

    //save to api
    this.actionsActive++;
    this.taskTemplateService.create(source).subscribe(newTaskTemplate => {
      this.taskTemplates.push(newTaskTemplate);
      this.actionsActive--;
    });

    //allow to add new directly
    this.editContainer = new TaskTemplate();
    this.actionsActive--;
  }

  public abort() {
    this.editSource = null;
    this.editContainer = null;
  }

  public save(source: TaskTemplate, target: TaskTemplate) {
    //lock
    this.actionsActive++;

    //write props
    target.intervalInDays = source.intervalInDays;
    target.name = source.name;
    target.reward = source.reward;

    //lock & persist changes
    this.actionsActive++;
    this.taskTemplateService.update(target).subscribe(() => this.actionsActive--);

    //stop edit
    this.abort();
    this.actionsActive--;
  }

  public remove(subject: TaskTemplate) {
    //lock
    this.actionsActive++;

    //lock & remove entity
    this.actionsActive++;
    this.taskTemplateService.remove(subject).subscribe(() => {
      this.taskTemplates.splice(this.taskTemplates.indexOf(subject), 1);
      this.actionsActive--;
    });

    //stop edit
    this.abort();
    this.actionsActive--;
  }

  public prepareExecution(taskTemplate: TaskTemplate) {
    this.executeSource = taskTemplate;
  }

  public abortExecution() {
    this.executeSource = null;
  }

  public confirmExecution(taskTemplate: TaskTemplate) {
    //lock
    this.actionsActive++;

    //register execution
    this.actionsActive++;
    this.taskTemplateService.registerExecution(taskTemplate).subscribe(() => {
      //remove from array
      this.taskTemplates.splice(this.taskTemplates.indexOf(taskTemplate), 1);

      //insert at correct location
      var relativeCompletion = this.calculateExpectedRelativeCompletion(taskTemplate);
      var added = false;
      for (let i = 0; i < this.taskTemplates.length; i++) {
        if (this.calculateExpectedRelativeCompletion(this.taskTemplates[i]) < relativeCompletion) {
          this.taskTemplates.splice(i, 0, taskTemplate);
          added = true;
          break;
        }
      }
      //add if not added in loop
      if (!added) {
        this.taskTemplates.push(taskTemplate);
      }

      this.actionsActive--;
    });

    //stop execution
    this.abortExecution();
    this.actionsActive--;
  }

  trackByFn(index) {
    return index;
  }
}
