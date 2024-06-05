import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouteHistoryService } from '../services/route-history.service';
import { VehicleService } from '../services/vehicle.service';
import { Gvar } from '../models/gvar';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
 
@Component({
  selector: 'app-route-history-form',
  templateUrl: './route-history-form.component.html',
  styleUrls: ['./route-history-form.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ]
})
export class RouteHistoryFormComponent implements OnInit {
  vehicles: any[] = [];
  routeHistoryForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private routeHistoryService: RouteHistoryService,
    private vehicleService: VehicleService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.routeHistoryForm = this.fb.group({
      vehicleID: ['', Validators.required],
      vehicleDirection: ['', Validators.required],
      status: ['', Validators.required],
      vehicleSpeed: ['', Validators.required],
      epoch: ['', Validators.required],
      address: ['', Validators.required],
      latitude: ['', Validators.required],
      longitude: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {
    this.vehicleService.getVehicles().subscribe((data: Gvar) => {
      if (data.dicOfDT && data.dicOfDT['Vehicles']) {
        this.vehicles = data.dicOfDT['Vehicles'];
      }
    });
  }

  submitRouteHistory(): void {
    const routeHistoryData: Gvar = {
      dicOfDic: {
        Tags: {
          VehicleID: this.routeHistoryForm.value.vehicleID.toString()
        }
      },
      dicOfDT: {
        RouteHistory: [this.routeHistoryForm.value]
      }
    };

   
    this.routeHistoryService.addRouteHistory(routeHistoryData).subscribe(() => {
      this.snackBar.open('Route history added successfully', 'Close', {
        duration: 3000
      });
      this.router.navigate(['/route-history']);
    }, error => {
      this.snackBar.open('Failed to add route history', 'Close', {
        duration: 3000
      });
      console.error('Error adding route history:', error);
    });
  }
}
