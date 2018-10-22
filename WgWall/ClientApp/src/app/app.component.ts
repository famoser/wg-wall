import { Component } from '@angular/core';

import { FrontendUser } from './models/frontend-user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  public selectedUser: FrontendUser | null;
}
