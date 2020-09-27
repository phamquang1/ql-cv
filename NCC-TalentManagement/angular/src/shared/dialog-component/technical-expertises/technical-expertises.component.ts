import { Component, OnInit, Inject, AfterContentChecked, Injector } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { CommonService } from '../../../app/services/common.service';
import { isBuffer } from 'lodash';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MyProfileService } from '@app/services/my-profile-service';
import { Level, findErr } from '@app/services/model/year-plan';
import { TechnicalExpertise, GroupTechnical, CvSkill } from '@app/services/model/my-profile-dto';
import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/session/app-session.service';
import { TranslateService } from '@ngx-translate/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-technical-expertises',
  templateUrl: './technical-expertises.component.html',
  styleUrls: ['./technical-expertises.component.css']
})
export class TechnicalExpertisesComponent extends AppComponentBase implements OnInit, AfterContentChecked {
  technicalForm: FormGroup;
  technicalEditForm: FormGroup;
  listGroupSkill: any;
  listSkill: any;
  listData: TechnicalExpertise;
  advancedFiltersVisible: boolean;
  listLevel = Level.DanhSach;
  listSkillChild = [];
  title: string;
  idGroupSkill: any;
  isSale: boolean;
  isUser: boolean;
  isEmployee = false;
  id: number;
  matOption: boolean = false;
  public findErr = findErr
  constructor(
    injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _dialogRef: MatDialogRef<TechnicalExpertisesComponent>,
    private fb: FormBuilder,
    private commonService: CommonService,
    private myProfileService: MyProfileService,
    private _permissionChecker: PermissionCheckerService,
    private session: AppSessionService,
    private translate: TranslateService
  ) { 
    super(injector);
  }
  ngAfterContentChecked() {
  }
  ngOnInit(): void {
    if (this.isEmployee === false) {
      this.id = this.session.userId;
    }
    this.isSale = this._permissionChecker.isGranted('Pages.EditAsSales.Employee')
    // this.isUser = this.session.userId === Number(this.id);
    this.getComboboxGroupSkill();
    this.advancedFiltersVisible = false;
    this.listData = this.data[0];
    this.isUser = this.data[1];
    if (!this.listData.groupSkills.length) {
      this.title = 'child1.technicalDialog.title';
      this.addgroupSkills();
    } else {
      this.title = 'child1.technicalDialog.title';
    }
  }

  getComboboxGroupSkill() {
    this.commonService.getComboboxGroupSkill().subscribe(res => {
      this.listGroupSkill = res.result;
    });
  }
  saveExpertise() {
    if (this.isSale && !this.isUser) {
      this._dialogRef.close(this.listData);
    } else {
      this.myProfileService.updateTechnicalExpertise(this.listData).subscribe(res => {
        this._dialogRef.close(res);
      }, err => {
        this.notify.error(this.translate.instant(this.findErr(err)));
      })
    }

  }
  saveItemafterEdit() {
    this.listSkillChild.forEach(el => {
      if (el.id === this.technicalEditForm.controls.id.value) {
        el.skillName = this.technicalEditForm.controls.id.value;
        el.level = this.technicalEditForm.controls.level.value;
      }
    });
  }
  changeIdGroupSkill(event: any) {
    this.commonService.getSkillByGroupSkillId(event).subscribe(res => {
      this.listSkill = res.result;
    });
  }
  getSkillByGroupSkillId(id: number) {
    this.listSkill = []
    this.commonService.getSkillByGroupSkillId(id).subscribe(res => {
      this.listSkill = res.result;
    });
  }
  changeGroupSkillId(id: number, item: GroupTechnical) {
    let index = this.listData.groupSkills.indexOf(item)
    this.listData.groupSkills[index].groupSkillId = id;
    this.getSkillByGroupSkillId(id);
  }
  changeListSkillId(id: number, item: CvSkill, data: GroupTechnical) {
    let index = this.listData.groupSkills.indexOf(data);
    let i = this.listData.groupSkills[index].cvSkills.indexOf(item);
    this.listData.groupSkills[index].cvSkills[i].skillId = id;
  }
  deleteCvSkill(GroupTechnical: GroupTechnical, cvSkill: CvSkill) {
    let indexOfGroupTechnical = this.listData.groupSkills.indexOf(GroupTechnical);
    let indexOfCvSkill = this.listData.groupSkills[indexOfGroupTechnical].cvSkills.indexOf(cvSkill);
    this.listData.groupSkills[indexOfGroupTechnical].cvSkills.splice(indexOfCvSkill, 1);
  }
  addgroupSkills() {
    this.listData.groupSkills.push({
      groupSkillId: '',
      name: '',
      cvSkills: [
        {
          id: null,
          level: null,
          order: null,
          skillId: null,
          skillName: ''
        }
      ]
    } as GroupTechnical);
  }
  addCvSkill(GroupTechnical: GroupTechnical) {
    let indexOfGroupTechnical = this.listData.groupSkills.indexOf(GroupTechnical);
    let data = {
      id: 0,
      order: null
    } as CvSkill
    this.listData.groupSkills[indexOfGroupTechnical].cvSkills.push(data);
  }
}
