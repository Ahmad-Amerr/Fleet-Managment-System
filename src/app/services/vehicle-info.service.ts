import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gvar } from '../models/gvar';

@Injectable({
  providedIn: 'root'
})
export class VehicleInformationService {
  private apiUrlInfo = 'https://localhost:7149/api/vehicleinfo';

  constructor(private http: HttpClient) {}

  addVehicleInformation(gvar: Gvar): Observable<any> {
    return this.http.post(`${this.apiUrlInfo}/add`, gvar);
  }

  updateVehicleInformation(gvar: Gvar): Observable<any> {
    console.log("api",gvar)
    return this.http.patch(`${this.apiUrlInfo}/update`, gvar);
  }

    deleteVehicleInformation(gvar: Gvar): Observable<any> {
    return this.http.request<any>('delete', `${this.apiUrlInfo}/delete`, { body: gvar });
  }
}
