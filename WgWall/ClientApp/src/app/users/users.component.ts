import { Component, ViewChild, Output, EventEmitter } from '@angular/core';
import { FrontendUserService } from '../services/frontend_user.service';
import { FrontendUser } from '../models/frontend-user';
import { NewUserComponent } from '../new-user/new-user.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent {
  public users: FrontendUser[]
  public selectedUser: FrontendUser
  private storageKey = "selectedFrontendUserId";
  @Output() userSelected = new EventEmitter<FrontendUser>();

  constructor(private frontendUserService: FrontendUserService) { }

  ngOnInit() {
    this.frontendUserService.get().subscribe(users => {
      this.users = users;

      const selectedId = Number(localStorage.getItem(this.storageKey));
      this.selectedUser = this.users.filter(u => u.id === selectedId)[0];
    });
  }

  @ViewChild(NewUserComponent)
  private newUserComponent: NewUserComponent;

  onCreateUser(frontendUser: FrontendUser) {
    if (this.users.filter(u => u.name === frontendUser.name).length === 0) {
      this.frontendUserService.create(frontendUser).subscribe(fu => {
        this.users.push(fu);
        this.newUserComponent.reset();
      });
    }
  }

  onSelectUser(frontendUser: FrontendUser) {
    this.userSelected.emit(frontendUser);
    this.selectedUser = frontendUser;
    localStorage.setItem(this.storageKey, frontendUser.id.toString());
  }
}
