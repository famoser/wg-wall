import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { TaskTemplate } from '../models/task-template';
import { map, switchMap } from 'rxjs/operators';
import { ReloadService } from './reload.service';
import { FrontendUserService } from './frontend-user.service';

@Injectable({ providedIn: 'root' })
export class TaskTemplateService {

  private taskTemplateUrl = 'api/TaskTemplate'; // URL to web api

  constructor(private http: HttpClient, private reload: ReloadService, private frontendUserService: FrontendUserService) {
  }

  get(): Observable<TaskTemplate[]> {
    return this.reload.reloadObservable.pipe(
      switchMap(() => this.http.get<TaskTemplate[]>(this.taskTemplateUrl))
    )
  }

  create(taskTemplate: TaskTemplate): Observable<TaskTemplate> {
    //todo: how to add to get() observable?
    return this.frontendUserService.getActiveUser().pipe(
      switchMap(frontendUser => {
        return this.http.post<TaskTemplate>(this.taskTemplateUrl, {
          frontendUserId: frontendUser.id,
          name: taskTemplate.name,
          intervalInDays: taskTemplate.intervalInDays
        });
      }),
      map((newTaskTemplate) => {
        taskTemplate.id = newTaskTemplate.id;
        return taskTemplate;
      })
    );
  }

  update(taskTemplate: TaskTemplate): Observable<void> {
    return this.frontendUserService.getActiveUser().pipe(
      switchMap(frontendUser => {
        return this.http.put<TaskTemplate>(this.taskTemplateUrl + "/" + taskTemplate.id, {
          frontendUserId: frontendUser.id,
          name: taskTemplate.name,
          intervalInDays: taskTemplate.intervalInDays,
          hide: taskTemplate.hide
        });
      }),
      map(() => { })
    );
  }

  registerExecution(taskTemplate: TaskTemplate): Observable<void> {
    return this.frontendUserService.getActiveUser().pipe(
      switchMap(frontendUser => {
        return this.http.post(this.taskTemplateUrl + "/executed/" + taskTemplate.id, {
          frontendUserId: frontendUser.id
        });
      }),
      map(() => { taskTemplate.lastExecutionAt = new Date() })
    );
  }
}
