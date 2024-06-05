import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gvar } from '../models/gvar';

@Injectable({
  providedIn: 'root'
})
export class RouteHistoryService {
  private apiUrl = 'https://localhost:7149/api/routehistory';

  constructor(private http: HttpClient) { }
  getRouteHistory(gvar: Gvar): Observable<Gvar> {
    return this.http.post<Gvar>(`${this.apiUrl}/get`, gvar);
  }


  addRouteHistory(gvar: Gvar): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, gvar);
  }
}
