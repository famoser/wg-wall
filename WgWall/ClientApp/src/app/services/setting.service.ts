import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Setting } from '../models/setting';

@Injectable({ providedIn: 'root' })
export class SettingService {

  private settingUrl = 'api/Setting';
  private settings$: Observable<Setting[]> = this.http.get<Setting[]>(this.settingUrl).pipe(shareReplay(1));
  private newSettings: Setting[] = [];

  constructor(private http: HttpClient) { }

  public get(key: string, def: string = ""): Observable<Setting> {
    return this.settings$.pipe(map(s => {
      let setting = s.filter(s => s.key == key)[0];
      if (setting) {
        return setting;
      }

      let newSetting = this.newSettings.filter(s => s.key == key)[0];
      if (newSetting) {
        return newSetting;
      }

      setting = new Setting();
      setting.key = key;
      setting.value = def;
      this.newSettings.push(setting);

      return setting;
    }));
  }

  public save(setting: Setting): Observable<any> {
    var payload = {
      key: setting.key,
      value: setting.value
    }

    //put or post depending if it exists
    if (setting.id > 0) {
      return this.http.put(this.settingUrl + "/" + setting.id, payload);
    }
    return this.http.post<Setting>(this.settingUrl, payload).pipe(
      map(answer => {
        setting.id = answer.id;
      })
    );
  }
}
