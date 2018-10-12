import { HttpClient } from "@angular/common/http";


@Injectable({ providedIn: 'root' })
export class TransportService {
    
    private baseApiUrl = "http://transport.opendata.ch/v1/stationboard?limit=4&station="
    constructor(private http: HttpClient) {
    }

    public get() : Vehicle[] {
        this.http.get(this.baseApiUrl);
        
    }
}