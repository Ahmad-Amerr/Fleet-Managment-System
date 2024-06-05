import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RouteHistoryFormComponent } from './route-history-form.component';

describe('RouteHistoryFormComponent', () => {
  let component: RouteHistoryFormComponent;
  let fixture: ComponentFixture<RouteHistoryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouteHistoryFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RouteHistoryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
