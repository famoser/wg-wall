import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Component, OnInit } from '@angular/core';
import {
  faCheck, faPencilAlt, faPlus, faSave, faTimes, faTrash
} from '@fortawesome/free-solid-svg-icons';

import { TaskTemplate } from '../models/task-template';
import { TaskTemplateService } from '../services/task-template.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html'
})
export class TasksComponent implements OnInit {
  //icons
  public faCheck = faCheck;
  public faTrash = faTrash;
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faTimes = faTimes;
  public faPlus = faPlus;

  //task lists
  public taskTemplates: TaskTemplate[];

  //input
  public editTaskTemplate: TaskTemplate = new TaskTemplate();
  public isEditActive: boolean = false;

  constructor(private taskTemplateService: TaskTemplateService) { }

  ngOnInit() {
    this.taskTemplateService.get().subscribe(taskTemplates => {
      this.taskTemplates = taskTemplates;
    });
  }

  registerExecution(taskTemplate: TaskTemplate) {
    this.taskTemplateService.registerExecution(taskTemplate).subscribe();
  }

  //edit stuff
  saveEditTaskTemplate() {
    if (!this.editTaskTemplate.id) {
      this.taskTemplateService.create(this.editTaskTemplate).subscribe((newTaskTemplate => {
        this.taskTemplates.push(newTaskTemplate);
      }));
    } else {
      this.taskTemplateService.update(this.editTaskTemplate).subscribe();
    }
    this.editTaskTemplate = new TaskTemplate();
    this.isEditActive = false;
  }

  remove(taskTemplate: TaskTemplate) {
    this.taskTemplateService.remove(taskTemplate).subscribe();
  }

  startEditTaskTemplate(taskTemplate: TaskTemplate) {
    this.editTaskTemplate = taskTemplate;
    this.startEdit();
  }

  abortEdit() {
    this.isEditActive = false;
  }

  startEdit() {
    this.isEditActive = true;
  }

  trackByFn(index) {
    return index;
  }
}
