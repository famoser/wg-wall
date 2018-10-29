import { Component, OnInit } from '@angular/core';
import { faUtensils } from '@fortawesome/free-solid-svg-icons';
import { FrontendUserService } from '../services/frontend-user.service';
import { FrontendUser } from '../models/frontend-user';
import { Plate } from '../models/plate';
import { forkJoin, Subject } from 'rxjs';
import { PlateService } from '../services/plate.service';
import { distinctUntilChanged, debounceTime, switchMap, debounce } from 'rxjs/operators';

@Component({
  selector: 'app-dinner',
  templateUrl: './dinner.component.html',
  styleUrls: ['./dinner.component.css']
})
export class DinnerComponent implements OnInit {
  //icons
  public faUtensils = faUtensils;
  public plates: Plate[];
  private selectedUser: FrontendUser;

  constructor(private frontendUserService: FrontendUserService, private plateService: PlateService) { }

  public ngOnInit() {
    this.frontendUserService.getActiveUser().subscribe((selectedUser) => {
      this.selectedUser = selectedUser;
    });
    //fetch entries
    this.frontendUserService.get().subscribe((frontendUsers) => {
      this.plateService.get().subscribe((dinners) => {
        let plates: Plate[] = [];
        frontendUsers.forEach(user => {
          let existing = dinners.filter(d => d.frontendUserId === user.id)[0];
          if (!existing) {
            existing = new Plate();
            existing.dinnerState = 0;
          }
          existing.frontendUser = user;
          plates.push(existing);
        })
        this.plates = plates;
      });
    });
  }

  public plateSelected() {
    let myPlate = this.plates.filter(d => d.frontendUser.id === this.selectedUser.id)[0];
    if (myPlate) {
      myPlate.dinnerState = (myPlate.dinnerState + 1) % 3;
      if (myPlate.id) {
        this.plateService.update(myPlate).subscribe();
      } else {
        this.plateService.create(myPlate).subscribe();
      }
    }
  }
}
