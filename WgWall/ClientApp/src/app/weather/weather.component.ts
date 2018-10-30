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
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

//implements the tooltip.onlyShowForDatasetIndex option
Chart.plugins.register({
  // need to manipulate tooltip visibility before its drawn (but after update)
  beforeDraw: function (chartInstance, easing) {
    if (chartInstance.config.options.tooltips.onlyShowForDatasetIndex) {
      var tooltipsToDisplay = chartInstance.config.options.tooltips.onlyShowForDatasetIndex;

      // get the active tooltip (if there is one)
      var active = chartInstance.tooltip._active || [];
      if (active.length > 0) {
        if (tooltipsToDisplay.indexOf(active[0]._datasetIndex) === -1) {
          // we don't want to show this tooltip so set it's opacity back to 0
          chartInstance.tooltip._model.opacity = 0;
          chartInstance.tooltip._model.width = 0;
          chartInstance.tooltip._model.height = 0;
          chartInstance.tooltip._model.x = -20;
          chartInstance.tooltip._model.y = -20;
          console.log(easing);
        }
      }
    }
  }
});

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
  public chartHtml: SafeHtml = "";

  //input
  public isEditActive: boolean = false;

  constructor(private weatherService: WeatherService, private settingService: SettingService, public store: Store, private sanitizer: DomSanitizer) { }

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
      var canvas = document.getElementById('weather');
      const chart = new Chart(canvas, {
        data: {
          labels: weatherEntries.map(we => moment(we.timestamp, "X").format("HH:mm")),
          datasets: [{
            label: 'temparature',
            type: 'line',
            yAxisID: 'temparature',
            xAxisID: 'time',
            data: weatherEntries.map(we => we.temparature),
            backgroundColor: 'rgba(255, 193, 7, 0)',
            borderColor: "rgba(200, 133, 3, 0.4)",
            pointHitRadius: 10
          }, {
            label: 'feels-like temparature',
            type: 'line',
            yAxisID: 'temparature',
            xAxisID: 'time',
            data: weatherEntries.map(we => we.perceivedTemparature),
            backgroundColor: 'rgba(255, 193, 7, 0.2)',
            pointBackgroundColor: 'rgba(0,0,0,0)',
            pointBorderColor: 'rgba(0,0,0,0)',
            borderColor: 'rgba(0,0,0,0)',
            pointHitRadius: 10
          }, {
            label: 'precipation',
            type: 'line',
            yAxisID: 'precipationProbability',
            xAxisID: 'time',
            data: weatherEntries.map(we => we.precipationProbability),
            backgroundColor: 'rgba(0, 123, 255, 0.2)',
            borderColor: "rgba(0, 80, 200, 0.4)",
            pointHitRadius: 10
          }, {
            type: 'bar',
            label: false,
            xAxisID: 'time',
            yAxisID: 'precipationProbability',
            backgroundColor: 'rgba(255, 255, 255, 0)',
            borderColor: 'rgba(255, 255, 255, 0)',
            borderWidth: 0,
            pointHitRadius: 0,
            data: weatherEntries.map(() => 99)
          }, {
            type: 'bar',
            label: 'cloudiness',
            xAxisID: 'time',
            yAxisID: 'precipationProbability',
            labelStyle: "background-image: linear-gradient(to right, rgb(216,230,254), rgb(108,115,127));",
            backgroundColor: weatherEntries.map(we => 1 - (we.cloudiness / 100) ** 2).map(we => (0.5 * (1 - we)) + we).map(per => 'rgb(' + 216 * per + ',' + 230 * per + ',' + 254 * per + ')'),
            data: weatherEntries.map(() => 100)
          }]
        },
        options: {
          tooltips: {
            onlyShowForDatasetIndex: [0, 1, 2],
          },
          legend: {
            display: false
          },
          legendCallback: function (chart) {
            var text = [];
            text.push('<ul class="chart-legend">');
            for (var i = 0; i < chart.data.datasets.length; i++) {
              if (chart.data.datasets[i].label) {
                let style = chart.data.datasets[i].labelStyle;
                if (!style) {
                  style = 'background-color:' + chart.data.datasets[i].backgroundColor + ";" +
                    'border: solid 1px ' + chart.data.datasets[i].borderColor;
                }
                text.push('<li><span class="chart-legend-preview" style="' + style + '"></span>');
                text.push(chart.data.datasets[i].label);
                text.push('</li>');
              }
            }
            text.push('</ul>');
            return text.join('');
          },
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
              stacked: true,
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
              id: "time",
              stacked: true,
              barPercentage: 1,
              categoryPercentage: 1,
              ticks: {
                autoSkip: true,
                maxTicksLimit: 8
              }
            }]
          }
        }
      });
      this.chartHtml = this.sanitizer.bypassSecurityTrustHtml(chart.generateLegend());
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
