import { Component, Input } from '@angular/core';
import {
    faCheck, faEyeSlash, faPencilAlt, faSave, faTimes, faTrash
} from '@fortawesome/free-solid-svg-icons';

import { FrontendUser } from '../models/frontend-user';
import { Task } from '../models/task';
import { TaskTemplate } from '../models/task-template';
import { TaskTemplateService } from '../services/task-template.service';
import { TaskService } from '../services/task.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html'
})
export class TasksComponent {
  //icons
  public faCheck = faCheck;
  public faTrash = faTrash;
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faEyeSlash = faEyeSlash;
  public faTimes = faTimes;

  //task lists
  public activeTasks: Task[] = [];
  public activeTaskTemplates: TaskTemplate[] = [];
  private taskTemplates: TaskTemplate[];

  //input
  public editTaskTemplate: TaskTemplate = new TaskTemplate();
  public isEditActive: boolean = false;

  @Input() user: FrontendUser;

  constructor(private taskService: TaskService, private taskTemplateService: TaskTemplateService) { }

  ngOnInit() {
    this.taskTemplateService.get().subscribe(tt => {
      this.taskTemplates = tt;
      this.activeTaskTemplates = this.taskTemplates.filter(t => !t.hide);

      this.taskService.get().subscribe(tasks => {
        tasks.forEach(t => {
          t.taskTemplate = this.taskTemplates.filter(tt => tt.id == t.taskTemplateId)[0];
        })
        this.activeTasks = tasks;
      })
    });
  }

  addTask(taskTemplate: TaskTemplate) {
    //create task    
    this.taskService.create(taskTemplate, this.user).subscribe(fu => {
      fu.taskTemplate = taskTemplate;
      this.activeTasks.push(fu);
    });
  }

  deleteTask(task: Task) {
    this.taskService.delete(task);
    this.activeTasks.splice(this.activeTasks.indexOf(task), 1);
  }

  markTaskAsDone(task: Task) {
    this.taskService.done(task, this.user);
    this.activeTasks.splice(this.activeTasks.indexOf(task), 1);
  }

  saveEditTaskTemplate() {
    if (!this.editTaskTemplate.id) {
      this.taskTemplateService.create(this.editTaskTemplate, this.user).subscribe(tt => this.activeTaskTemplates.push(tt));
    } else {
      this.taskTemplateService.update(this.editTaskTemplate, this.user);
    }
    this.editTaskTemplate = new TaskTemplate();
    this.isEditActive = false;
  }

  hideTaskTemplate(taskTemplate: TaskTemplate) {
    this.taskTemplateService.hide(taskTemplate);
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
}
