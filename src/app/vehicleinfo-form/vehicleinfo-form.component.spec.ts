import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleInformationFormComponent } from './vehicleinfo-form.component';

describe('VehicleinfoFormComponent', () => {
  let component: VehicleInformationFormComponent;
  let fixture: ComponentFixture<VehicleInformationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleInformationFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehicleInformationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
