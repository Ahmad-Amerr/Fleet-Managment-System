import { Component, OnInit } from '@angular/core';
import { RouteHistoryService } from '../services/route-history.service';
import { Gvar } from '../models/gvar';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { VehicleService } from '../services/vehicle.service';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-route-list',
  templateUrl: './route-list.component.html',
  styleUrls: ['./route-list.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatTableModule,
    MatButtonModule,
    DatePipe
  ]
})
export class RouteListComponent implements OnInit {
  routeHistories: any[] = [];
  vehicles: any[] = [];
  searchForm!: FormGroup;
  displayedColumns: string[] = ['vehicleID', 'vehicleDirection', 'status', 'vehicleSpeed', 'epoch', 'address', 'latitude', 'longitude'];

  constructor(
    private routeHistoryService: RouteHistoryService,
    private vehicleService: VehicleService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      vehicleID: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required]
    });

    this.vehicleService.getVehicles().subscribe((data: Gvar) => {
      console.log(data);
      if (data.dicOfDT && data.dicOfDT['Vehicles']) {
        this.vehicles = data.dicOfDT['Vehicles'];
      }
    });
  }

  onSearch(): void {
    const formValue = this.searchForm.getRawValue();
    const gvar: Gvar = {
      dicOfDic: {
        Tags: {
          VehicleID: formValue.vehicleID,
          StartTime: formValue.startTime,
          EndTime: formValue.endTime
        }
      },
      dicOfDT: {}
    };

    this.routeHistoryService.getRouteHistory(gvar).subscribe((data: Gvar) => {
      if (data.dicOfDT && data.dicOfDT['RouteHistory']) {
        this.routeHistories = data.dicOfDT['RouteHistory'].map((route: any) => ({
          
          ...route,
          Epoch: this.convertEpochToDate(route.epoch)

        }));
      }
    });
  }
  convertEpochToDate(epoch: number): string {
    console.log(epoch);
    const date = new Date(epoch * 1000); 
    return date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
    
  }
}
