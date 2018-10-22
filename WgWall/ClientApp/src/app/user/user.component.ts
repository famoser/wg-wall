import { Component, EventEmitter, Input, Output } from '@angular/core';

import { FrontendUser } from '../models/frontend-user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html'
})
export class UserComponent {
  @Input() user: FrontendUser;
  @Input() selectedUser: FrontendUser;

  @Output() selected = new EventEmitter<FrontendUser>()

  clicked() {
    this.selected.emit() 
  }
}
