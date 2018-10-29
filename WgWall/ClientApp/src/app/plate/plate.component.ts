import { Component, OnInit } from '@angular/core';
import { faUtensils } from '@fortawesome/free-solid-svg-icons';
import { FrontendUserService } from '../services/frontend-user.service';
import { FrontendUser } from '../models/frontend-user';
import { Plate } from '../models/plate';
import { forkJoin } from 'rxjs';
import { PlateService } from '../services/plate.service';

@Component({
  selector: 'app-dinner',
  templateUrl: './dinner.component.html',
  styleUrls: ['./dinner.component.css']
})
export class DinnerComponent implements OnInit {

  //icons
  public faUtensils = faUtensils;
  public plates: Plate[];
  public selectedUser: FrontendUser;

  constructor(private frontendUserService: FrontendUserService, private plageService: PlateService) { }

  ngOnInit(): void {
    this.frontendUserService.getActiveUser().subscribe((selectedUser) => {
      this.selectedUser = selectedUser;
      forkJoin(
        this.plageService.get(),
        this.frontendUserService.get()
      ).subscribe(([dinners, frontendUsers]) => {
        let plates: Plate[];
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
      this.save(myPlate)
    }
  }

  private save(plate: Plate) {
    if (plate.id) {
      this.plageService.update(plate);
    } else {
      this.plageService.create(plate);
    }
  }
}
