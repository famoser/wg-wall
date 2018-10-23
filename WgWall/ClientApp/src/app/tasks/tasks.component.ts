import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Component, Input, OnInit } from '@angular/core';
import {
    faCheck, faEyeSlash, faPencilAlt, faPlus, faSave, faTimes, faTrash, faEye
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
  public faEyeSlash = faEyeSlash;
  public faEye = faEye;
  public faTimes = faTimes;
  public faPlus = faPlus;

  //task lists
  private taskTemplates$ : Observable<TaskTemplate[]>;
  public activeTaskTemplates$ : Observable<TaskTemplate[]>;

  //input
  public editTaskTemplate: TaskTemplate = new TaskTemplate();
  public isEditActive: boolean = false;

  constructor(private taskTemplateService: TaskTemplateService) { }

  ngOnInit() {
    this.taskTemplates$ = this.taskTemplateService.get();
    this.activeTaskTemplates$ = this.taskTemplates$.pipe(
      map(taskTemplate => taskTemplate.filter(tt => !tt.hidden))
    );
  }

  registerExecution(taskTemplate: TaskTemplate) {
    this.taskTemplateService.registerExecution(taskTemplate).subscribe();
  }

  //edit stuff
  saveEditTaskTemplate() {
    if (!this.editTaskTemplate.id) {
      this.taskTemplateService.create(this.editTaskTemplate).subscribe();
    } else {
      this.taskTemplateService.update(this.editTaskTemplate).subscribe();
    }
    this.editTaskTemplate = new TaskTemplate();
    this.isEditActive = false;
  }

  toggleHidden(taskTemplate: TaskTemplate) {
    taskTemplate.hidden = true;
    this.taskTemplateService.update(taskTemplate).subscribe();
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
