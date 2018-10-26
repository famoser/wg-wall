import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ReloadService } from './reload.service';
import { WeatherEntry } from '../models/weather-entry';

@Injectable({ providedIn: 'root' })
export class WeatherService {
  private baseApiUrl = "https://api.weatherbit.io/v2.0/forecast/hourly?country=ch"

  constructor(private http: HttpClient, private reload: ReloadService) {
  }

  public get(postalCode: string, key: string): Observable<WeatherEntry[]> {
    let link = this.baseApiUrl + "&key=" + key + "&postal_code=" + postalCode;
    return this.reload.reloadObservable.pipe(
      switchMap(() => {
        return this.http.get(link).pipe(map(weatherJson => {
          let weatherEntries: WeatherEntry[] = [];
          weatherJson["data"].forEach(element => {
            let weatherEntry = new WeatherEntry();
            weatherEntry.timestamp = element["ts"];
            weatherEntry.temparature = element["temp"];
            weatherEntry.perceivedTemparature = element["app_temp"];
            weatherEntry.precipationProbability = element["pop"];
            weatherEntry.cloudiness = element["clouds"];
            weatherEntry.cloudSeverity = element["weather"]["code"] < 800 ? 1 : weatherEntry.cloudiness * 0.5;
            weatherEntries.push(weatherEntry);
          });
          return weatherEntries;
        }));
      }));
  };
}
