// src/app/models/vehicle.ts
export class Vehicle {
  vehicleID!: number ;
  vehicleNumber: string | undefined;
  vehicleType: string | undefined;
  lastDirection?: string; 
  lastStatus?: string;
  lastAddress?: string;
  lastLatitude?: number;
  lastLongitude?: number;
}

  