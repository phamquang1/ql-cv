import { Component, OnInit ,Inject, Injector} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EducationDto } from '@app/services/model/my-profile-dto';
import {MyProfileService} from '../../../app/services/my-profile-service'
import { Degree } from '@app/services/model/year-plan';
import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppComponentBase } from '@shared/app-component-base';
import { TranslateService } from '@ngx-translate/core';
import { findErr} from '../../../app/services/model/year-plan'



@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css'],
})
export class EducationComponent extends AppComponentBase implements OnInit {
  education: EducationDto;
  listDegree = Degree.DanhSach;
  title: string;
  isSale: boolean;
  isUser: boolean;
  isEmployee = false;
  id: number;
  public findErr = findErr;

  constructor(
    injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private myProfileService: MyProfileService,
    private _dialogRef: MatDialogRef<EducationComponent>,
    private _permissionChecker: PermissionCheckerService,
    private session: AppSessionService,
    private translate: TranslateService
  ) { 
    super(injector);
  }

  ngOnInit(): void {
    if (this.isEmployee === false) {
      this.id = this.session.userId;
    }
    this.isSale = this._permissionChecker.isGranted('Pages.EditAsSales.Employee');
    this.education = this.data[0];
    this.isUser = this.data[1];
    if (this.education.id !== 0) {
      this.title = 'child1.educationDialog.edit';
    } else {
      this.title = 'child1.educationDialog.create';
    }
    this.filterDegree();
  }
  filterDegree() {
    this.listDegree.options.forEach(item => {
      if (this.education.degreeType === item.value) {
        this.education.degreeType = item.id;
      }
    });
  }
  validate(condition,err){
    if(condition){
      this.notify.error(this.translate.instant(err))
      return;
    }
  }
  submitEducation() {
    if(Number(this.education.endYear) - Number(this.education.startYear) < 0){
      this.notify.error(this.translate.instant('child1.errDialog.err1'))
      return;
    }
    if(!this.education.startYear){
      this.notify.error(this.translate.instant('child1.errDialog.err2'));
      return ;
    }
    if(!this.education.endYear){
      this.notify.error(this.translate.instant('child1.errDialog.err3'));
      return ;
    }
    if(!this.education.schoolOrCenterName){
      this.notify.error(this.translate.instant('child1.errDialog.err4'));
      return ;
    }
    if(!this.education.major){
      this.notify.error(this.translate.instant('child1.errDialog.err5'));
      return ;
    }
    
    
    if (this.isSale && !this.isUser) {
      this._dialogRef.close(this.education);
    } else {
      this.myProfileService.SaveEducation(this.education).subscribe(res => {
        this._dialogRef.close(res);
      }, err => {
        this.notify.error(this.translate.instant(this.findErr(err)));
      });
    }
  }

}
