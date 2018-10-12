import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Subscription } from 'rxjs'

import { Setting } from '../models/setting';

@Injectable({ providedIn: 'root' })
export class SettingService {

    private settingUrl = 'api/Setting';
    private settings: Setting[] = [];
    private settingSubscription: Subscription;
    private settingLoaded: Boolean = false;


    constructor(private http: HttpClient) {
        this.initSettings();
    }

    initSettings() {
        this.settingSubscription = this.http.get<Setting[]>(this.settingUrl).subscribe(s => {
            if (this.settings == null) {
                this.settings = s;
                this.settingLoaded = true;
            }
        });
    }

    get(key: String, def: String = ""): Setting {
        //need to wait here; I've asked for help: https://stackoverflow.com/questions/52778759/wait-on-rxjs-subscription-before-resuming
        
        return this.resolveSetting(key, def);
    }

    private resolveSetting(key: String, def: String) {
        let setting = this.settings.filter(s => s.key == key)[0];
        if (!(setting instanceof Setting)) {
            setting = new Setting();
            setting.key = key;
            setting.value = def;
            this.settings.push(setting);
        }
        return setting;
    }

    save(setting: Setting): void {
        this.http.post(this.settingUrl, setting).subscribe(() => {});
    }
}
