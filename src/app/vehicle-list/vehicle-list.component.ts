// src/app/vehicle-list/vehicle-list.component.ts
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { VehicleInformationService } from '../services/vehicle-info.service';
import { VehicleInformationFormComponent } from '../vehicleinfo-form/vehicleinfo-form.component';
import { VehicleService } from '../services/vehicle.service'; 
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { Gvar } from '../models/gvar';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    HttpClientModule,
    MatButtonModule,
    MatTableModule,
    MatIconModule,
    VehicleInformationFormComponent
  ],
  providers: [VehicleInformationService, VehicleService]
})
export class VehicleListComponent implements OnInit {
  vehicles!: any[];

  constructor(
    private vehicleInfoService: VehicleInformationService,
    private vehicleService: VehicleService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadVehicleInformations();
  }

  loadVehicleInformations(): void {
    this.vehicleService.getVehicles().subscribe((data: Gvar) => {
      if (data.dicOfDT && data.dicOfDT['Vehicles']) {
        this.vehicles = data.dicOfDT['Vehicles'];
      }
    });
  }

  addVehicleInformation(): void {
    const dialogRef = this.dialog.open(VehicleInformationFormComponent, {
      width: '400px',
      data: { isEditMode: false }
    });

    dialogRef.afterClosed().subscribe(() => this.loadVehicleInformations());
  }

  editVehicleInformation(vehicleInformationID: number): void {
    const dialogRef = this.dialog.open(VehicleInformationFormComponent, {
      width: '400px',
      data: { isEditMode: true, vehicleInformationID }
    });

    dialogRef.afterClosed().subscribe(() => this.loadVehicleInformations());
  }

  deleteVehicleInformation(vehicleID: number): void {
    console.log("vehicle id",vehicleID)
    const gvar: Gvar = {
      dicOfDic: {
        Tags: {
          VehicleID: vehicleID.toString()
        }
      },
      dicOfDT: {}
    };

    this.vehicleInfoService.deleteVehicleInformation(gvar).subscribe(() => {
      this.loadVehicleInformations();
    });
  }
}
