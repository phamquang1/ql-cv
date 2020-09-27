import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkingExperiencesComponent } from './working-experiences.component';

describe('WorkingExperiencesComponent', () => {
  let component: WorkingExperiencesComponent;
  let fixture: ComponentFixture<WorkingExperiencesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkingExperiencesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkingExperiencesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
