import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Vehicle } from '../models/vehicle';
import { Gvar } from '../models/gvar';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = 'https://localhost:7149/vehicle/vehicles';
  private apiUrlInfo = 'http://localhost:7149/api/vehicleinfo';

  constructor(private http: HttpClient) { }

  getVehicles(): Observable<Gvar> {
    return this.http.get<Gvar>(`${this.apiUrl}/Get`);
  }
  

  getVehicleInformationDetails(gvar: Gvar): Observable<Gvar> {
    return this.http.post<Gvar>(`${this.apiUrl}/GetDetails`, gvar);
  }

  addVehicleInformation(gvar: Gvar): Observable<any> {
    return this.http.post(`${this.apiUrlInfo}/add`, gvar);
  }

  updateVehicleInformation(gvar: Gvar): Observable<any> {
    return this.http.post(`${this.apiUrlInfo}/update`, gvar);
  }

}
