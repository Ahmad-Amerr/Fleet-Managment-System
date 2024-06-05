// src/app/vehicle-information-form/vehicle-information-form.component.ts
import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { VehicleInformationService } from '../services/vehicle-info.service';
import { Gvar } from '../models/gvar';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule } from '@angular/material/core';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicleinfo-form',
  templateUrl: './vehicleinfo-form.component.html',
  styleUrls: ['./vehicleinfo-form.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatNativeDateModule,
    ReactiveFormsModule
  ]
})
export class VehicleInformationFormComponent implements OnInit {
  vehicleInformationForm!: FormGroup;
  isEditMode: boolean = false;
  vehicleInformationID!: number;

  constructor(
    private fb: FormBuilder,
    private vehicleInfoService: VehicleInformationService,
    private VehicleService:VehicleService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<VehicleInformationFormComponent>
  ) {
    if (data) {
      this.isEditMode = data.isEditMode;
      this.vehicleInformationID = data.vehicleInformationID;
    }
  }

  ngOnInit(): void {
    this.vehicleInformationForm = this.fb.group({
      vehicleID: ['', Validators.required],
      driverID: ['', Validators.required],
      vehicleMake: ['', Validators.required],
      vehicleModel: ['', Validators.required],
      purchaseDate: ['', Validators.required],
      vehicleNumber: ['', Validators.required],
      vehicleType: ['', Validators.required]
    });

    if (this.isEditMode && this.vehicleInformationID) {
      console.log(this.vehicleInformationID)
      this.loadVehicleInformationDetails(this.vehicleInformationID);
    }
  }
  loadVehicleInformationDetails(vehicleInformationID: number): void {
    const gvar: Gvar = {
      dicOfDic: {
        Tags: {
          VehicleID: vehicleInformationID.toString()
        }
      },
      dicOfDT: {}
    };

    this.VehicleService.getVehicleInformationDetails(gvar).subscribe((data: Gvar) => {
      const vehicleInfo = data.dicOfDT['VehicleDetails'];
      console.log(vehicleInfo)
      this.vehicleInformationForm.patchValue({
        vehicleID: vehicleInformationID,
        driverID: vehicleInfo[0]['driverID'],
        vehicleMake: vehicleInfo[0]['vehicleMake'],
        vehicleModel: vehicleInfo[0]['vehicleModel'],
        purchaseDate: vehicleInfo[0]['purchaseDate'],
        vehicleNumber: vehicleInfo[0]['vehicleNumber'],
        vehicleType: vehicleInfo[0]['vehicleType']
      });
    });
  }



  save(): void {
    const gvar: Gvar = {
      dicOfDic: {
        Tags: {
          VehicleID: this.vehicleInformationForm.get('vehicleID')?.value,
          DriverID: this.vehicleInformationForm.get('driverID')?.value,
          VehicleMake: this.vehicleInformationForm.get('vehicleMake')?.value,
          VehicleModel: this.vehicleInformationForm.get('vehicleModel')?.value,
          PurchaseDate: this.vehicleInformationForm.get('purchaseDate')?.value,
          VehicleNumber: this.vehicleInformationForm.get('vehicleNumber')?.value,
          VehicleType: this.vehicleInformationForm.get('vehicleType')?.value,
        }
      },
      dicOfDT: {}
    };

    if (this.isEditMode) {
      console.log("editmode",gvar.dicOfDic)
      this.vehicleInfoService.updateVehicleInformation(gvar).subscribe(() => {
        this.dialogRef.close();
      });
    } else {
      this.vehicleInfoService.addVehicleInformation(gvar).subscribe(() => {
        this.dialogRef.close();
      });
    }
  }
}
