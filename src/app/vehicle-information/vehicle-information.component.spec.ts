import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleInformationListComponent } from './vehicle-information.component';

describe('VehicleInformationComponent', () => {
  let component: VehicleInformationListComponent;
  let fixture: ComponentFixture<VehicleInformationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleInformationListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehicleInformationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
