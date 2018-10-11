import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { FrontendUser } from '../models/frontend-user';

@Injectable({ providedIn: 'root' })
export class FrontendUserService {

  private frontendUserUrl = 'api/FrontendUser'; // URL to web api

  constructor(private http: HttpClient) {
  }

  get(): Observable<FrontendUser[]> {
    return this.http.get<FrontendUser[]>(this.frontendUserUrl);
  }

  create(frontendUser: FrontendUser): Observable<FrontendUser> {
    return this.http.post<FrontendUser>(this.frontendUserUrl, frontendUser);
  }
}
