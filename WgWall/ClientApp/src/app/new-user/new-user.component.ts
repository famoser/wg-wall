import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { FrontendUser } from '../models/frontend-user';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.css']
})
export class NewUserComponent implements OnInit {
  @Output() create = new EventEmitter<FrontendUser>();
  public user: FrontendUser;
  public isOpen: boolean = false;

  ngOnInit() {
    this.user = new FrontendUser();
  }

  open() {
    this.isOpen = !this.isOpen;
  }

  createUser() {
    this.create.emit(this.user);
  }

  reset() {
    this.isOpen = false;
    this.user = new FrontendUser();
  }
}
