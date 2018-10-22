import { Observable, BehaviorSubject } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';

import { FrontendUser } from '../models/frontend-user';

@Injectable({ providedIn: 'root' })
export class FrontendUserService {

  private frontendUserUrl = 'api/FrontendUser'; // URL to web api
  private activeUser$: BehaviorSubject<FrontendUser>

  constructor(private http: HttpClient) {
    this.activeUser$ = new BehaviorSubject<FrontendUser>(null);
  }

  public get(): Observable<FrontendUser[]> {
    return this.http.get<FrontendUser[]>(this.frontendUserUrl);
  }

  public create(frontendUser: FrontendUser): Observable<FrontendUser> {
    return this.http.post<FrontendUser>(this.frontendUserUrl, frontendUser);
  }

  public setActiveUser(frontendUser: FrontendUser) : void {
    this.activeUser$.next(frontendUser);
  }

  public getActiveUser(): Observable<FrontendUser> {
    return this.activeUser$;
  }
}
