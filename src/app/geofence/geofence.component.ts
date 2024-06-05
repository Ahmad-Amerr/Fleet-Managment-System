import { Component, OnInit } from '@angular/core';
import { GeofenceService } from '../services/geofence.service';
import { Gvar } from '../models/gvar';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-geofence',
  templateUrl: './geofence.component.html',
  styleUrls: ['./geofence.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatFormFieldModule,
    MatSelectModule
  ]
})
export class GeofenceComponent implements OnInit {
  geofences: any[] = [];
  displayedColumns: string[] = [
    'geofenceID',
    'geofenceType',
    'addedDate',
    'strokeColor',
    'strokeOpacity',
    'strokeWeight',
    'fillColor',
    'fillOpacity'
  ];

  constructor(private geofenceService: GeofenceService) {}

  ngOnInit(): void {
    this.geofenceService.getGeofences().subscribe((data: Gvar) => {
      if (data.dicOfDT && data.dicOfDT['Geofences']) {
        this.geofences = data.dicOfDT['Geofences'];
      }
    });
  }
}
