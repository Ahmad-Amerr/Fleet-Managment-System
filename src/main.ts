import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter, Routes } from '@angular/router';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { VehicleListComponent } from './app/vehicle-list/vehicle-list.component';
import { VehicleDetailsComponent } from './app/vehicle-details/vehicle-details.component';
import { VehicleInformationFormComponent } from './app/vehicleinfo-form/vehicleinfo-form.component';
import { provideHttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { DriversComponent } from './app/drivers/drivers.component';
import { RouteListComponent } from './app/route-list/route-list.component';
import { RouteHistoryFormComponent } from './app/route-history-form/route-history-form.component';
import { GeofenceComponent } from './app/geofence/geofence.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

const routes: Routes = [
  { path: '', redirectTo: '/vehicles', pathMatch: 'full' },
  { path: 'vehicles', component: VehicleListComponent },
  { path: 'vehicle/add', component: VehicleInformationFormComponent },
  { path: 'vehicle/edit/:id', component: VehicleInformationFormComponent },
  { path: 'vehicle/details/:id', component: VehicleDetailsComponent },
  { path: 'drivers', component: DriversComponent },
  { path: 'route-list', component: RouteListComponent },
  { path: 'add-route-history', component: RouteHistoryFormComponent },  
  { path:'goefence',component:GeofenceComponent}
];

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    importProvidersFrom(BrowserAnimationsModule,ReactiveFormsModule), provideAnimationsAsync()
  ]
}).catch(err => console.error(err));
