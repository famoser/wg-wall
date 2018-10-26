import { Component } from '@angular/core';
import { faPencilAlt, faSave, faUndo } from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment';

import { Setting } from '../models/setting';
import { SettingService } from '../services/setting.service';
import { Store } from '../store.service';
import { WeatherService } from '../services/weather.service';
import { forkJoin, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Chart } from 'chart.js'

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: [
    './weather.component.css'
  ]
})
export class WeatherComponent {
  //icons
  public faPencilAlt = faPencilAlt;
  public faSave = faSave;
  public faUndo = faUndo;

  //properties
  public postalCode = new Configuration("weather.postal_code", "8053");
  public apiKey = new Configuration("weather.api_key");

  //input
  public isEditActive: boolean = false;

  constructor(private weatherService: WeatherService, private settingService: SettingService, public store: Store) { }

  ngOnInit() {
    forkJoin(
      this.settingService.get(this.postalCode.key, this.postalCode.defaultValue).pipe(tap(setting => { this.postalCode.assingSetting(setting); })),
      this.settingService.get(this.apiKey.key).pipe(tap(setting => { this.apiKey.assingSetting(setting); }))
    ).subscribe(() => {
      if (this.apiKey.value && this.postalCode.value) {
        this.retrieveWeather();
      }
    });
  }

  private retrieveWeather() {
    this.weatherService.get(this.postalCode.value, this.apiKey.value).subscribe(weatherEntries => {
      this.store.setWeatherEntries(weatherEntries);

      var canvas = document.getElementById('weather');
      new Chart(canvas, {
        type: 'line',
        data: {
          labels: weatherEntries.map(we => moment(we.timestamp, "X").format("HH:mm")),
          datasets: [{
            label: 'temparature',
            yAxisID: 'temparature',
            data: weatherEntries.map(we => we.temparature),
            backgroundColor: 'rgba(255, 193, 7, 0)',
            borderColor: "rgba(200, 133, 3, 0.4)",
            pointHitRadius: 10
          }, {
            label: 'feels-like temparature',
            yAxisID: 'temparature',
            data: weatherEntries.map(we => we.perceivedTemparature),
            backgroundColor: 'rgba(255, 193, 7, 0.2)',
            pointBackgroundColor: 'rgba(0,0,0,0)',
            pointBorderColor: 'rgba(0,0,0,0)',
            borderColor: 'rgba(0,0,0,0)',
            pointHitRadius: 10
          }, {
            label: 'precipation',
            yAxisID: 'precipationProbability',
            data: weatherEntries.map(we => we.precipationProbability),
            backgroundColor: 'rgba(0, 123, 255, 0.2)',
            borderColor: "rgba(0, 80, 200, 0.4)",
            pointHitRadius: 10
          }]
        },
        options: {
          scales: {
            yAxes: [{
              id: 'temparature',
              type: 'linear',
              position: 'left',
              gridLines: {
                display: false
              },
              ticks: {
                fontColor: "rgba(200, 133, 3, 0.6)",
                callback: function (value) {
                  return value + "°";
                }
              }
            }, {
              id: 'precipationProbability',
              type: 'linear',
              position: 'right',
              ticks: {
                min: 0,
                max: 100,
                autoSkip: true,
                maxTicksLimit: 5,
                fontColor: "rgba(0, 80, 200, 0.6)",
                callback: function (value) {
                  return value + "%";
                }
              }
            }],
            xAxes: [{
              ticks: {
                autoSkip: true,
                maxTicksLimit: 8
              }
            }]
          }
        }
      });
    });
  }

  public save() {
    this.settingService.save(this.apiKey.setEditValue()).subscribe();
    this.settingService.save(this.postalCode.setEditValue()).subscribe();
    this.retrieveWeather();

    this.isEditActive = false;
  }

  public abort() {
    this.isEditActive = false;
  }

  public startEdit() {
    this.apiKey.resetEditValue();
    this.postalCode.resetEditValue();
    this.isEditActive = true;
  }
}

class Configuration {
  public editValue;
  public setting;

  constructor(public key: string, public defaultValue: string = null) {
    this.editValue = this.defaultValue;
  }

  public assingSetting(setting: Setting) {
    this.setting = setting;
  }

  public get value(): string {
    return this.setting.value;
  }

  public resetEditValue() {
    this.editValue = this.setting.value;
  }

  public setEditValue(): Setting {
    this.setting.value = this.editValue;
    return this.setting;
  }
}
