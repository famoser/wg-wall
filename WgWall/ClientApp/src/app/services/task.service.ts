import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Subscription, Observable } from 'rxjs'
import { FrontendUser } from '../models/frontend-user';
import { Task } from '../models/task';
import { TaskTemplate } from '../models/task-template';

@Injectable({ providedIn: 'root' })
export class TaskService {

    private taskUrl = 'api/Task'; // URL to web api

    constructor(private http: HttpClient) {
    }
  
    get(): Observable<Task[]> {
      return this.http.get<Task[]>(this.taskUrl);
    }
  
    create(taskTemplate: TaskTemplate, frontendUser: FrontendUser): Observable<Task> {
      return this.http.post<Task>(this.taskUrl + "/create/" + taskTemplate.id, {
        frontendUserId: frontendUser.id
      });
    }
  
    done(task: Task, frontendUser: FrontendUser) {
      this.http.post(this.taskUrl + "/done/" + task.id, {
          frontendUserId: frontendUser.id
      }).subscribe(() => {});
    }
  
    delete(task: Task) {
      this.http.delete(this.taskUrl + "/" + task.id).subscribe(() => {});
    }
}
