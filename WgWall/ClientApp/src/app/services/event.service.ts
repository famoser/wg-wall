import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { EventEntity } from '../models/event';
import { ReloadService } from './reload.service';
import { switchMap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class EventService {

  private eventUrl = 'api/Event'; // URL to web api

  constructor(private http: HttpClient, private reload: ReloadService) {
  }

  public get(): Observable<EventEntity[]> {
    return this.reload.reloadObservable.pipe(
      switchMap(() => this.http.get<EventEntity[]>(this.eventUrl))
    )
  }

  public create(event: EventEntity): Observable<EventEntity> {
    return this.http.post<EventEntity>(this.eventUrl, this.toJsonPayload(event));
  }

  public update(event: EventEntity): Observable<any> {
    return this.http.put(this.eventUrl + "/" + event.id, this.toJsonPayload(event));
  }

  public remove(event: EventEntity): Observable<any> {
    return this.http.delete(this.eventUrl + "/" + event.id);
  }

  private toJsonPayload(event: EventEntity) {
    return {
      name: event.name,
      startDate: event.startDate
    };
  }
}
