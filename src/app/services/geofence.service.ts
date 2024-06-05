import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gvar } from '../models/gvar';

@Injectable({
  providedIn: 'root'
})
export class GeofenceService {
  private apiUrl = 'https://localhost:7149/api/routehistory/geofence';

  constructor(private http: HttpClient) {}

  getGeofences(): Observable<Gvar> {
    return this.http.get<Gvar>(`${this.apiUrl}/getAll`);
  }
}
