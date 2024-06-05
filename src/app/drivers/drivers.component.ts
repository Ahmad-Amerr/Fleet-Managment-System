import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DriverService } from '../services/driver.service';
import { Gvar } from '../models/gvar';
import { DriverFormComponent } from '../driver-form/driver-form.component';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html',
  styleUrls: ['./drivers.component.css'],
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatDialogModule]
})
export class DriversComponent implements OnInit {
  drivers: any[] = [];
  displayedColumns: string[] = ['driverName', 'phoneNumber', 'actions'];

  constructor(private driverService: DriverService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadDrivers();
  }

  loadDrivers(): void {
    this.driverService.getDrivers().subscribe((data: Gvar) => {
      if (data.dicOfDT && data.dicOfDT['Drivers']) {
        this.drivers = data.dicOfDT['Drivers'];
      }
    });
  }

  addDriver(): void {
    const dialogRef = this.dialog.open(DriverFormComponent, {
      width: '400px',
      data: { isEditMode: false }
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        this.loadDrivers();
      }
    });
  }

  editDriver(driverid: any): void {
    console.log(driverid);
    const dialogRef = this.dialog.open(DriverFormComponent, {
      width: '400px',
      data: { isEditMode: true, driverid }
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        this.loadDrivers();
      }
    });
  }

  deleteDriver(driverID: number): void {
    const gvar: Gvar = {
      dicOfDic: {
        Tags: {
          DriverID: driverID.toString()
        }
      },
      dicOfDT: {}
    };

    this.driverService.deleteDriver(gvar).subscribe(() => {
      this.loadDrivers();
    });
  }
}




