import { Component } from '@angular/core';
import { FrontendUserService } from '../services/frontend_user.service';
import { FrontendUser } from '../models/frontend-user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent {
  constructor(private frontendUserService: FrontendUserService) {}
  
  ngOnInit() {
    this.getFrontendUsers();
  }

  getFrontendUsers(): void {
    this.frontendUserService.getFrontendUsers()
    .subscribe(users => this.users = users);
  }

  public users : FrontendUser[]
}
