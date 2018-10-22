import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { FrontendUser } from '../models/frontend-user';
import { TaskTemplate } from '../models/task-template';

@Injectable({ providedIn: 'root' })
export class TaskTemplateService {

    private taskTemplateUrl = 'api/TaskTemplate'; // URL to web api

    constructor(private http: HttpClient) {
    }
  
    get(): Observable<TaskTemplate[]> {
      return this.http.get<TaskTemplate[]>(this.taskTemplateUrl);
    }
  
    create(taskTemplate: TaskTemplate, frontendUser: FrontendUser): Observable<TaskTemplate> {
      return this.http.post<TaskTemplate>(this.taskTemplateUrl, {
        frontendUserId: frontendUser.id,
        name: taskTemplate.name,
        intervalInDays: taskTemplate.intervalInDays
      });
    }
  
    update(taskTemplate: TaskTemplate, frontendUser: FrontendUser): Observable<TaskTemplate> {
      return this.http.put<TaskTemplate>(this.taskTemplateUrl + "/" + taskTemplate.id, {
        frontendUserId: frontendUser.id,
        name: taskTemplate.name,
        intervalInDays: taskTemplate.intervalInDays
      });
    }
  
    hide(taskTemplate: TaskTemplate) {
      this.http.get(this.taskTemplateUrl + "/hide/" + taskTemplate.id).subscribe(() => {});
    }
}
