import { BehaviorSubject, Observable } from 'rxjs';

import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ReloadService {

  private reloadSubject: BehaviorSubject<any>;

  constructor() {
    this.reloadSubject = new BehaviorSubject(null);
  }

  get reloadObservable(): Observable<any> {
    return this.reloadSubject;
  }

  public reload() {
    this.reloadSubject.next(null);
  }
}
