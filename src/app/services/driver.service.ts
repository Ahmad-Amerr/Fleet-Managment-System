import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gvar } from '../models/gvar';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private apiUrl = 'https://localhost:7149/api/driver';

  constructor(private http: HttpClient) { }

  getDrivers(): Observable<Gvar> {
    return this.http.get<Gvar>(`${this.apiUrl}/get`);
  }
  getDriverDetails(gvar: Gvar): Observable<Gvar> {
    return this.http.post<Gvar>(`${this.apiUrl}/GetDetails`, gvar);
  }
  addDriver(gvar: Gvar): Observable<any> {
    console.log(gvar);
    return this.http.post(`${this.apiUrl}/add`, gvar);
  }

  updateDriver(gvar: Gvar): Observable<any> {
    return this.http.post(`${this.apiUrl}/update`, gvar);
  }
  
  deleteDriver(gvar: Gvar): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: gvar
    };
    return this.http.delete(`${this.apiUrl}/delete`, options);
  }
}
