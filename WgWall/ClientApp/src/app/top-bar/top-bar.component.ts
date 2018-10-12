import { Component, Output, EventEmitter } from '@angular/core';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { FrontendUser } from '../models/frontend-user';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html'
})
export class TopBarComponent {
  faPlusCircle = faPlusCircle
  @Output("userSelected") userSelected = new EventEmitter<FrontendUser>();

  onUserSelected(frontendUser: FrontendUser) {
    this.userSelected.emit(frontendUser);
  }
}
