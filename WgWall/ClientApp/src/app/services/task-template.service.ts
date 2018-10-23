import { Observable, timer } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { TaskTemplate } from '../models/task-template';
import { map, switchMap, tap, debounce, startWith } from 'rxjs/operators';
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
    return this.http.post<TaskTemplate>(this.taskTemplateUrl, {
      name: taskTemplate.name,
      intervalInDays: taskTemplate.intervalInDays,
      reward: taskTemplate.reward
    }).pipe(
      tap((newTaskTemplate) => taskTemplate.id = newTaskTemplate.id)
    );
  }

  update(taskTemplate: TaskTemplate): Observable<any> {
    return this.http.put(this.taskTemplateUrl + "/" + taskTemplate.id, {
      name: taskTemplate.name,
      intervalInDays: taskTemplate.intervalInDays,
      reward: taskTemplate.reward
    });
  }

  remove(taskTemplate: TaskTemplate): Observable<any> {
    return this.http.delete(this.taskTemplateUrl + "/" + taskTemplate.id);
  }

  registerExecution(taskTemplate: TaskTemplate): Observable<any> {
    return this.frontendUserService.getActiveUser().pipe(
      switchMap(frontendUser => {
        return this.http.post(this.taskTemplateUrl + "/executed/" + taskTemplate.id, {
          frontendUserId: frontendUser.id
        });
      }),
      tap(() => { taskTemplate.lastExecutionAt = new Date() })
    );
  }
}
