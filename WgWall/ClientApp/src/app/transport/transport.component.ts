import { Component } from '@angular/core';
import { faPencilAlt, faSave, faTimes } from '@fortawesome/free-solid-svg-icons';
import { Vehicle } from '../models/vehicle';
import { TransportService } from '../services/transport.service';
import { Setting } from '../models/setting';
import { SettingService } from '../services/setting.service';

@Component({
  selector: 'app-transport',
  templateUrl: './transport.component.html'
})
export class TransportComponent {
  //icons
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faTimes = faTimes;

  //properties
  public stationName: string;
  public vehicles: Vehicle[];
  private settingKey = "trasport.service.station";
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

  refreshVehicles(stationName: string) {
    this.transportService.get(stationName).subscribe(vh => {
      this.vehicles = vh;
    });
  }

  save() {
    this.setting.value = this.stationName;
    this.settingService.save(this.setting);
    this.isEditActive = false;

    this.refreshVehicles(this.stationName);
  }

  abort() {
    this.stationName = this.setting.value;
    this.isEditActive = false;
  }

  enableEdit() {
    this.isEditActive = true;
  }
}
