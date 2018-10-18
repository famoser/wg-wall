import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Setting } from '../models/setting';

@Injectable({ providedIn: 'root' })
export class SettingService {

    private settingUrl = 'api/Setting';
    private settings$: Observable<Setting[]> = this.http.get<Setting[]>(this.settingUrl).pipe(shareReplay(1));

    constructor(private http: HttpClient) { }

    get(key: string, def: string = ""): Observable<Setting> {
        return this.settings$.pipe(map(s => {
            let setting = s.filter(s => s.key == key)[0];
            if (!(setting instanceof Setting)) {
                setting = new Setting();
                setting.key = key;
                setting.value = def;
                s.push(setting);
            }
            return setting;
        }));
    }

    save(setting: Setting): void {
        this.http.post(this.settingUrl, setting).subscribe(() => 1);
    }
}
