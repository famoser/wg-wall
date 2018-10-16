import { HttpClient } from "@angular/common/http";
import { SettingService } from "./setting.service";
import { Vehicle } from "../models/vehicle";
import { Observable } from "rxjs";
import { map } from 'rxjs/operators'
import { Injectable } from "@angular/core";
import { Setting } from "../models/setting";


@Injectable({ providedIn: 'root' })
export class TransportService {
    private baseApiUrl = "https://transport.opendata.ch/v1/stationboard?limit=4&station="

    constructor(private http: HttpClient) {
    }

    public get(stationName: string): Observable<Vehicle[]> {
        return this.http.get(this.baseApiUrl + stationName).pipe(map(transportJson => {
            let vehicles = [];
            transportJson["stationboard"].forEach(element => {
                let vehicle = new Vehicle();
                vehicle.name = element["category"] + " " + element["number"];
                vehicle.departureTime = new Date(element["stop"]["departure"]);
                vehicles.push(vehicle);
            });
            return vehicles;
        }));
    };
}
