import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, Subscription, Subscribable } from 'rxjs'
import { of } from 'rxjs';
import { forkJoin } from 'rxjs';

import { Setting } from '../models/setting';
import { Settings } from 'http2';
import { setHostBindings } from '@angular/core/src/render3/instructions';

@Injectable({ providedIn: 'root' })
export class SettingService {

    private settingUrl = 'api/Setting';
    private settings: Setting[] = [];
    private settingSubscription: Subscription;
    private settingLoaded: Boolean = false;


    constructor(private http: HttpClient) {
        this.initSettings();
    }

    initSettings(): Subscription {
        if (this.settingSubscription == null) {
            this.settingSubscription = this.http.get<Setting[]>(this.settingUrl).subscribe(s => {
                this.settings = s;
                this.settingLoaded = true;
            });
        }

        return this.settingSubscription;
    }

    get(key: string, def: String = ""): Observable<Setting> {
        return forkJoin()
        return this.initSettings().add(() =>  {

        });
        
        return of(this.resolveSetting(key, def));
    }

    private resolveSetting(key: string, def: String) {

        let setting = this.settings.filter(s => s.key == key)[0];
        if (!(setting instanceof Setting)) {
            setting = new Setting();
            setting.key = key;
            setting.value = key;
        }
        return setting;
    }

    create(setting: setting, frontendUser: FrontendUser): Observable<setting> {
        console.log(setting.name);
        return this.http.post<setting>(this.settingUrl, {
            name: setting.name,
            frontendUserId: frontendUser.id
        });
    }

    update(setting: setting): Observable<any> {
        return this.http.put(this.settingUrl + "/" + setting.id, setting);
    }
}
