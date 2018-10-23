import { BehaviorSubject, Observable, timer } from 'rxjs';

import { Injectable } from '@angular/core';
import { debounce } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ReloadService {

  private reloadSubject: BehaviorSubject<any>;

  constructor() {
    this.reloadSubject = new BehaviorSubject(null);
  }

  get reloadObservable(): Observable<any> {
    return this.reloadSubject.pipe(
      debounce(() => timer(200))
    );
  }

  public reload() {
    this.reloadSubject.next(null);
  }
}
