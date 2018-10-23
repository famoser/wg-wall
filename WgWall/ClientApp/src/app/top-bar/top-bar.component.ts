import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { faSync } from '@fortawesome/free-solid-svg-icons';

import { FrontendUser } from '../models/frontend-user';
import { ReloadService } from '../services/reload.service';
import { interval, Observable } from 'rxjs';
import { startWith, map, distinctUntilChanged } from 'rxjs/operators';
import * as moment from 'moment';
import { Moment } from 'moment';
import { FrontendUserService } from '../services/frontend-user.service';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ["./top-bar.component.css"]
})
export class TopBarComponent implements OnInit {
  pageLoaded: Moment;
  timeFromNow: Observable<string>;

  ngOnInit(): void {
    this.pageLoaded = moment(new Date());

    this.timeFromNow = interval(1000).pipe(
      startWith(),
      map(() => this.pageLoaded.fromNow()),
      distinctUntilChanged()
    );
  }
  public faSync = faSync;

  @Output() userSelected = new EventEmitter<FrontendUser>();

  constructor(private reloadService: ReloadService, private frontendUserService: FrontendUserService) { }

  onUserSelected(frontendUser: FrontendUser) {
    this.userSelected.emit(frontendUser);
    this.frontendUserService.setActiveUser(frontendUser);
  }


  reload() {
    this.reloadService.reload()
  }
}
