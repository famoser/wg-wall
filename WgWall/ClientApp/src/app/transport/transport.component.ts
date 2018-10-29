import { Component, OnInit } from '@angular/core';
import { faPencilAlt, faSave, faUndo } from '@fortawesome/free-solid-svg-icons';

import { Setting } from '../models/setting';
import { Vehicle } from '../models/vehicle';
import { SettingService } from '../services/setting.service';
import { TransportService } from '../services/transport.service';

@Component({
  selector: 'app-transport',
  templateUrl: './transport.component.html',
  styleUrls: ['./transport.component.css']
})
export class TransportComponent implements OnInit {
  //icons
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faUndo = faUndo;

  //properties
  public stationName: string;
  public newStationName: string;
  public vehicles: Vehicle[];
  private settingKey = "transport.station-name";
  private setting: Setting;

  //input
  public isEditActive: boolean = false;

  constructor(private transportService: TransportService, private settingService: SettingService) { }

  ngOnInit() {
    this.settingService.get(this.settingKey, "Waserstrasse").subscribe(s => {
      this.setting = s;
      this.stationName = s.value;
      this.refreshVehicles(this.stationName);
    });
  }

  private refreshVehicles(stationName: string) {
    this.transportService.get(stationName).subscribe(vh => {
      this.vehicles = vh;
    });
  }

  private setNewStationName(stationName: string) {
    this.stationName = stationName;
    this.setting.value = stationName;
    this.settingService.save(this.setting).subscribe();
  }

  public save() {
    if (this.newStationName != this.stationName) {
      //save changes & refresh
      this.setNewStationName(this.newStationName);
      this.refreshVehicles(this.newStationName);
    }
    this.isEditActive = false;
  }

  public abort() {
    this.isEditActive = false;
  }

  public startEdit() {
    this.newStationName = this.stationName;
    this.isEditActive = true;
  }
}
