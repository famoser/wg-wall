import { HttpClient } from "@angular/common/http";
import { SettingService } from "./setting.service";
import { Vehicle } from "../models/vehicle";
import { Observable } from "rxjs";
import { map } from 'rxjs/operators'
import { Injectable } from "@angular/core";


@Injectable({ providedIn: 'root' })
export class TransportService {
    private baseApiUrl = "http://transport.opendata.ch/v1/stationboard?limit=4&station="
    private settingKey = "trasport.service.station";

    constructor(private http: HttpClient, private settingService: SettingService) {
    }

    public get(): Observable<Vehicle[]> {
        this.http.get(this.baseApiUrl + this.settingService.get(this.settingKey)).pipe(map(transportJson => {
            console.log(transportJson)
            return [new Vehicle()]
        }));
    };
}