import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FrontendUser } from '../models/frontend-user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html'
})
export class UserComponent {
  @Input("user") user: FrontendUser;
  @Input("selectedUser") selectedUser: FrontendUser;

  @Output("selected") selected = new EventEmitter<FrontendUser>()

  clicked() {
    this.selected.emit() 
  }
}
