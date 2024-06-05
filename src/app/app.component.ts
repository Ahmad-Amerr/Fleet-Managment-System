// src/app/app.component.ts
import { Component } from '@angular/core';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material.module';
import { RouterModule } from '@angular/router';
import { DriversComponent } from './drivers/drivers.component';
import { RouteListComponent } from './route-list/route-list.component';
import { GeofenceComponent } from './geofence/geofence.component';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [
    MatToolbarModule,
    CommonModule,
    MatButtonModule,
    MatIconModule,
    HttpClientModule,
     VehicleListComponent,
     MaterialModule,
     RouterModule,
     DriversComponent,
     RouteListComponent,
     GeofenceComponent
  ]
})
export class AppComponent {
  title = 'Fleet Management';
}
