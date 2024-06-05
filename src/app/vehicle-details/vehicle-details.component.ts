import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.css'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatListModule, MatIconModule]
})
export class VehicleDetailsComponent implements OnInit {
  vehicleDetails: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<VehicleDetailsComponent>
  ) {
    this.vehicleDetails = data;
    console.log("constructor", this.vehicleDetails);
  }

  ngOnInit(): void {}

  closeDialog(): void {
    this.dialogRef.close();
  }
}
