import { Observable, timer } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { TaskTemplate } from '../models/task-template';
import { map, switchMap, tap, debounce, startWith } from 'rxjs/operators';
import { ReloadService } from './reload.service';
import { FrontendUserService } from './frontend-user.service';

@Injectable({ providedIn: 'root' })
export class TaskTemplateService {

  private taskExecution = 'api/TaskExecution'; // URL to web api
  private taskTemplateUrl = 'api/TaskTemplate'; // URL to web api

  constructor(private http: HttpClient, private reload: ReloadService, private frontendUserService: FrontendUserService) {
  }

  public get(): Observable<TaskTemplate[]> {
    return this.reload.reloadObservable.pipe(
      switchMap(() => this.http.get<TaskTemplate[]>(this.taskTemplateUrl))
    )
  }

  public create(taskTemplate: TaskTemplate): Observable<TaskTemplate> {
    //todo: how to add to get() observable?
    return this.http.post<TaskTemplate>(this.taskTemplateUrl, this.toJsonPayload(taskTemplate));
  }

  public update(taskTemplate: TaskTemplate): Observable<any> {
    return this.http.put(this.taskTemplateUrl + "/" + taskTemplate.id, this.toJsonPayload(taskTemplate));
  }

  public remove(taskTemplate: TaskTemplate): Observable<any> {
    return this.http.delete(this.taskTemplateUrl + "/" + taskTemplate.id);
  }

  public registerExecution(taskTemplate: TaskTemplate): Observable<any> {
    return this.frontendUserService.getActiveUser().pipe(
      switchMap(frontendUser => {
        return this.http.post(this.taskExecution, {
          frontendUserId: frontendUser.id,
          taskTemplateId: taskTemplate.id
        }).pipe(
          switchMap(() => {
            //adapt connected entites
            frontendUser.karma += taskTemplate.reward;
            taskTemplate.lastExecutionAt = new Date();
            return this.update(taskTemplate);
          })
        )
      })
    );
  }

  private toJsonPayload(taskTemplate: TaskTemplate) {
    return {
      name: taskTemplate.name,
      intervalInDays: taskTemplate.intervalInDays,
      reward: taskTemplate.reward
    };
  }
}
