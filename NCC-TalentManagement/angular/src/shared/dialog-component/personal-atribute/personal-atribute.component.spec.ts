import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalAtributeComponent } from './personal-atribute.component';

describe('PersonalAtributeComponent', () => {
  let component: PersonalAtributeComponent;
  let fixture: ComponentFixture<PersonalAtributeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonalAtributeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonalAtributeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
