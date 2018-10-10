import { Component, Input } from '@angular/core';
import { FrontendUser } from '../models/frontend-user';
import { FrontendUserService } from '../services/frontend_user.service';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.css']
})
export class NewUserComponent {
  public user: FrontendUser;
  public isOpen: boolean = false;

  constructor(private frontendUserService: FrontendUserService) {}

  ngOnInit() {
    this.user = new FrontendUser();
  }

  open() {
    this.isOpen = true;
  }

  createUser() {
    this.frontendUserService.create(this.user).subscribe(e => window.location.reload(), e => console.log(e));
  }
}
