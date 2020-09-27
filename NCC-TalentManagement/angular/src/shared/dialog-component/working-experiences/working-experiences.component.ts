import { Component, OnInit, Inject, Injector } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { WorkingExperienceDto } from '@app/services/model/my-profile-dto';
import { MyProfileService } from '../../../app/services/my-profile-service';
import { EmployeeService } from '../../../app/services/employee.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
// Depending on whether rollup is used, moment needs to be imported differently.
// Since Moment.js doesn't have a default export, we normally need to import using the `* as`
// syntax. However, rollup creates a synthetic default module and we thus need to import it using
// the `default as` syntax.
import * as _moment from 'moment';
import { default as _rollupMoment, Moment } from 'moment';
import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/session/app-session.service';
import { TranslateService } from '@ngx-translate/core';
import { AppComponentBase } from '@shared/app-component-base';
import { findErr } from '@app/services/model/year-plan';

const moment = _rollupMoment || _moment;
// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};
@Component({
  selector: 'app-working-experiences',
  templateUrl: './working-experiences.component.html',
  styleUrls: ['./working-experiences.component.css'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class WorkingExperiencesComponent extends AppComponentBase implements OnInit {
  title: string;
  workingExp: WorkingExperienceDto;
  currentlyWorking = false;
  listTechnologies: WorkingExperienceDto[] = [];
  isSale: boolean;
  isUser: boolean;
  isEmployee = false;
  id: number;
  stateForm: FormGroup;
  public findErr = findErr;
  constructor(
    injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private myProfileService: MyProfileService,
    private employeeService: EmployeeService,
    private _formBuilder: FormBuilder,
    private _permissionChecker: PermissionCheckerService,
    private _dialogRef: MatDialogRef<WorkingExperiencesComponent>,
    private session: AppSessionService,
    private translate: TranslateService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.isEmployee === false) {
      this.id = this.session.userId;
    }
    this.buildForm();
    this.isSale = this._permissionChecker.isGranted('Pages.EditAsSales.Employee')
    this.isUser = this.data[1];
    this.stateForm.patchValue({
      startTime: this.data[0].startTime,
      endTime: this.data[0].endTime,
      projectName: this.data[0].projectName,
      technologies: this.data[0].technologies,
      position: this.data[0].position,
      projectDescription: this.data[0].projectDescription,
      responsibility: this.data[0].responsibility,
      currentlyWorking: this.data[0].endTime ? false : true,
      id: this.data[0].id,
      userId: this.data[0].userId,
      order : this.data[0].order
    });
    console.log(this.data.id)
    if (this.data[0].id !== 0) {
      this.title = 'child1.workingDialog.edit';
    } else {
      this.title = 'child1.workingDialog.create';
    }
    // fake working
    this.stateForm.get('currentlyWorking').valueChanges.subscribe(res => {
      this.currentlyWorking = res;
    });
  }
  buildForm () {
    this.stateForm = this._formBuilder.group({
      startTime: [''],
      endTime: [''],
      projectName: [''],
      technologies: [''],
      position: [''],
      projectDescription: [''],
      responsibility: [''],
      currentlyWorking: [false],
      search: ['']
    });
  }

  searchByTechnologies(text: string) {
    this.employeeService.GetWorkingExperiencePaging(text).subscribe(res => {
      if (res) {
        this.listTechnologies = res.result;
      }
    });
  }
  selectedProjectName(work: WorkingExperienceDto) {
    this.stateForm.patchValue({
      startTime : work.startTime,
      endTime : work.endTime,
      projectName : work.projectName,
      technologies : work.technologies,
      position : work.position,
      projectDescription : work.projectDescription,
      responsibility : work.responsibility,
      currentlyWorking : work.endTime ? false : true,
      id : work.id,
      userId :  work.userId,
      order : work.order
    });
  }
  // end fake

  submitWorkingExp() {
    const formData =  this.stateForm.value;
    let data = {
      id : this.data[0].id,
      startTime : formData.startTime,
      endTime : this.currentlyWorking ? null : formData.endTime,
      projectName : formData.projectName,
      technologies : formData.technologies,
      position : formData.position,
      projectDescription : formData.projectDescription,
      responsibility : formData.responsibility,
      userId :  this.data[0].userId,
      order : this.data[0].order
    }
    if(!data.endTime){
      let endTime = new Date()
      if(endTime.getTime() - new Date(data.startTime).getTime() < 0){
        this.notify.error(this.translate.instant('child1.errDialog.err1'));
        return;
      }
    } else {
      if(new Date(data.endTime).getTime() - new Date(data.startTime).getTime() < 0){
        this.notify.error(this.translate.instant('child1.errDialog.err1'));
        return;
      }
    }

    if (this.isSale && !this.isUser) {
      this._dialogRef.close(data);
    } else {
      this.myProfileService.UpdateWorkingExperience(data).subscribe(res => {
        this._dialogRef.close(res);
      }, err => {
        this.notify.error(this.translate.instant(this.findErr(err)));
      });
    }
  }
}

export interface StateGroup {
  letter: string;
  names: string[];
}

export const _filter = (opt: string[], value: string): string[] => {
  const filterValue = value.toLowerCase();

  return opt.filter(item => item.toLowerCase().indexOf(filterValue) === 0);
};
