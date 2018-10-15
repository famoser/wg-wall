import { Component } from '@angular/core';
import { faPencilAlt } from '@fortawesome/free-solid-svg-icons';
import { Vehicle } from '../models/vehicle';
import { TransportService } from '../services/transport.service';
import { Setting } from '../models/setting';
import { SettingService } from '../services/setting.service';

@Component({
  selector: 'app-transport',
  templateUrl: './transport.component.html',
  styleUrls: ['./transport.component.css']
})
export class TransportComponent {
  //icons
  public faPencilAlt = faPencilAlt;

  //properties
  public stationName: string;
  public vehicles: Vehicle[];
  private settingKey = "trasport.service.station";
  private setting: Setting;

  //input
  public isEditActive: Boolean = false;

  constructor(private transportService: TransportService, private settingService: SettingService) { }

  ngOnInit() {
    this.settingService.get(this.settingKey, "Waserstrasse").subscribe(s => {
      this.setting = s;
      this.transportService.get(this.stationName).subscribe(vh => {
        this.vehicles = vh;
      });
    });
  }

  toggleEdit() {
    this.isEditActive = !this.isEditActive;
  }

  setStation(station: string): void {
    this.setting.value = station;
    this.settingService.save(this.setting);
    this.isEditActive = false;
  }
}
