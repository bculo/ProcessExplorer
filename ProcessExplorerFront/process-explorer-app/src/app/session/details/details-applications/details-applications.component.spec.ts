import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsApplicationsComponent } from './details-applications.component';

describe('DetailsApplicationsComponent', () => {
  let component: DetailsApplicationsComponent;
  let fixture: ComponentFixture<DetailsApplicationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailsApplicationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsApplicationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
