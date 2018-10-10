import { Component, Input } from '@angular/core';
import { FrontendUser } from '../models/frontend-user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html'
})
export class UserComponent {
  @Input("user") user: FrontendUser;
}
