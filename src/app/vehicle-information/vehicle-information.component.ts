import { Component, OnInit } from '@angular/core';
import { VehicleInformationService } from '../services/vehicle-info.service';
import { MatDialog } from '@angular/material/dialog';
import { VehicleInformationFormComponent } from '../vehicleinfo-form/vehicleinfo-form.component';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatListModule } from '@angular/material/list';
import { Gvar } from '../models/gvar';

@Component({
  selector: 'app-vehicle-information-list',
  templateUrl: './vehicle-information-list.component.html',
  styleUrls: ['./vehicle-information-list.component.css'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatDialogModule, MatListModule]
})
export class VehicleInformationListComponent implements OnInit {
  vehicleInformations: any[] = [];

  constructor(private vehicleInformationService: VehicleInformationService, public dialog: MatDialog) {}

  ngOnInit(): void {
    console.log("hi")
    this.loadVehicleInformations();
  }

  loadVehicleInformations(): void {
    // Implement a method to fetch and load vehicle information
  }

  addVehicleInformation(): void {
    const dialogRef = this.dialog.open(VehicleInformationFormComponent);
    dialogRef.componentInstance.isEditMode = false;
    dialogRef.afterClosed().subscribe(() => this.loadVehicleInformations());
  }
  editVehicleInformation(vehicleInformationID: number): void {
    const dialogRef = this.dialog.open(VehicleInformationFormComponent, {
      data: {
        isEditMode: true,
        vehicleInformationID: vehicleInformationID
      }
    });
  
    dialogRef.afterClosed().subscribe(() => this.loadVehicleInformations());
  }
  

  deleteVehicleInformation(vehicleInformationID: number): void {
    const request: Gvar = {
      dicOfDic: {
        Tags: {
          ID: vehicleInformationID.toString()
        }
      },
      dicOfDT: {}
    };
    this.vehicleInformationService.deleteVehicleInformation(request).subscribe(() => this.loadVehicleInformations());
  }
}
