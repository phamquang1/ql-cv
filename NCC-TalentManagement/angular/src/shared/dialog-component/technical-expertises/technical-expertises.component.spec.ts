import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TechnicalExpertisesComponent } from './technical-expertises.component';

describe('TechnicalExpertisesComponent', () => {
  let component: TechnicalExpertisesComponent;
  let fixture: ComponentFixture<TechnicalExpertisesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TechnicalExpertisesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalExpertisesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
