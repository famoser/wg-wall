import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { FrontendUser } from '../models/frontend-user';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class HeroService {

  private frontendUserUrl = 'api/FrontendUser'; // URL to web api

  constructor(
    private http: HttpClient) {
  }

  /** GET heroes from the server */
  getFrontendUsers(): Observable<FrontendUser[]> {
    return this.http.get<FrontendUser[]>(this.frontendUserUrl);
  }
}
