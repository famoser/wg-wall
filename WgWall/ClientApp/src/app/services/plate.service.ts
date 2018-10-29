import { Observable, BehaviorSubject } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Plate } from '../models/plate';
import { ReloadService } from './reload.service';
import { switchMap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class PlateService {

  private dinnerUrl = 'api/Plate';

  constructor(private http: HttpClient, private reload: ReloadService) {
  }

  public get(): Observable<Plate[]> {
    return this.reload.reloadObservable.pipe(
      switchMap(() => this.http.get<Plate[]>(this.dinnerUrl))
    )
  }

  public create(dinner: Plate): Observable<Plate> {
    return this.http.post<Plate>(this.dinnerUrl, this.toJsonPayload(dinner));
  }

  public update(dinner: Plate): Observable<any> {
    return this.http.put(this.dinnerUrl + "/" + dinner.id, this.toJsonPayload(dinner));
  }

  private toJsonPayload(dinner: Plate) {
    return {
      dinnerState: dinner.dinnerState,
      frontendUserId: dinner.frontendUser.id
    };
  }
}
