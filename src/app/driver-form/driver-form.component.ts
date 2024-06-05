import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DriverService } from '../services/driver.service';
import { Gvar } from '../models/gvar';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-driver-form',
  templateUrl: './driver-form.component.html',
  styleUrls: ['./driver-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule]
})
export class DriverFormComponent implements OnInit {
  driverForm!: FormGroup;
  isEditMode: boolean = false;
  driverID!: number;

  constructor(
    private fb: FormBuilder,
    private driverService: DriverService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<DriverFormComponent>
  ) { }

  ngOnInit(): void {
    this.isEditMode = this.data.isEditMode;
    this.driverID = this.data.driverid;
    console.log(this.driverID)
    this.driverForm = this.fb.group({
      driverName: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    });

    if (this.isEditMode && this.driverID) {
      this.loadDriverDetails(this.driverID);
    }
  }

  loadDriverDetails(driverID: number): void {
    const gvar: Gvar = {
      dicOfDic: {
        Tags: {
          DriverID: driverID.toString()
        }
      },
      dicOfDT: {}
    };

    this.driverService.getDriverDetails(gvar).subscribe(
      (data: Gvar) => {
        if (data.dicOfDT && data.dicOfDT['DriverDetails']) {
          const driverDetails = data.dicOfDT['DriverDetails'][0];
          console.log("driver details:",driverDetails);
          this.driverForm.patchValue({
            driverName: driverDetails.driverName,
            phoneNumber: driverDetails.phoneNumber,
       
          });
        }
      },
      (error) => {
        console.error('Error fetching driver details:', error);
      }
    );
  }

  onSave(): void {
    if (this.driverForm.valid) {
      console.log(this.driverID);
      const gvar: Gvar = {
        dicOfDic: {
          Tags: {
            DriverID: this.driverID?.toString() ,
            DriverName: this.driverForm.value.driverName,
            PhoneNumber: this.driverForm.value.phoneNumber
          }
        },
        dicOfDT: {}
      };

      if (this.isEditMode) {
        gvar.dicOfDic['Tags']['DriverID'] = this.driverID.toString();
        console.log(this.driverID);
        console.log(gvar);
        this.driverService.updateDriver(gvar).subscribe(() => {
          this.dialogRef.close(true);
        });      } else {
        this.driverService.addDriver(gvar).subscribe(() => this.dialogRef.close(true));
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
