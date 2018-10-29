import { Component, EventEmitter, Output } from '@angular/core';
import { faPencilAlt, faSave, faUndo, faPlus } from '@fortawesome/free-solid-svg-icons';

import { FrontendUser } from '../models/frontend-user';
import { FrontendUserService } from '../services/frontend-user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ["./users.component.css"]
})
export class UsersComponent {
  //icons
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faUndo = faUndo;
  public faPlus = faPlus;

  public users: FrontendUser[]
  public selectedUser: FrontendUser

  public editContainer: FrontendUser = null;

  private storageKey = "selectedFrontendUserId";
  @Output() userSelected = new EventEmitter<FrontendUser>();

  constructor(private frontendUserService: FrontendUserService) { }

  ngOnInit() {
    this.frontendUserService.get().subscribe(users => {
      this.users = users;

      const selectedId = Number(localStorage.getItem(this.storageKey));
      this.selectedUser = this.users.filter(u => u.id === selectedId)[0];
      if (this.selectedUser != null) {
        this.userSelected.emit(this.selectedUser);
      }
    });
  }

  startAdd() {
    this.editContainer = new FrontendUser();
  }

  startEdit(frontendUser: FrontendUser) {
    this.editContainer = new FrontendUser();
    this.editContainer.name = frontendUser.name;
    this.editContainer.id = frontendUser.id;
  }

  add(frontendUser: FrontendUser) {
    if (this.users.filter(u => u.name === frontendUser.name).length === 0) {
      this.frontendUserService.create(frontendUser).subscribe(fu => {
        this.users.push(fu);
      });
    }
  }

  abortEdit() {
    this.editContainer = null;
  }

  save() {
    //update
    if (this.editContainer.id > 0) {
      this.selectedUser.name = this.editContainer.name;
      this.frontendUserService.update(this.selectedUser).subscribe();
    } else {
      //or add
      this.add(this.editContainer);
    }
    this.abortEdit();
  }

  public select(frontendUser: FrontendUser) {
    this.abortEdit();
    this.userSelected.emit(frontendUser);
    this.selectedUser = frontendUser;
    localStorage.setItem(this.storageKey, frontendUser.id.toString());
  }
}
