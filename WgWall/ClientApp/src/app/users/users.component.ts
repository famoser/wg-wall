import { Component } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent {
  public users = [{name: "Florian", karma: "2000", profileImageSrc: null},{name: "Xenia", karma: "2200", profileImageSrc: null},{name: "Cedric", karma: "12", profileImageSrc: null}];
}
