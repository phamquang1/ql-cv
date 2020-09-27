import { Component, OnInit, Inject, Injector } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MyProfileService } from '@app/services/my-profile-service';
import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/session/app-session.service';
import { findErr } from '@app/services/model/year-plan';
import { TranslateService } from '@ngx-translate/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-personal-atribute',
  templateUrl: './personal-atribute.component.html',
  styleUrls: ['./personal-atribute.component.css']
})
export class PersonalAtributeComponent extends AppComponentBase implements OnInit {
  atributeForm: FormGroup;
  body;
  listData;
  title: string;
  foundItem: number;
  isSale: boolean;
  isUser: boolean;
  id: number;
  public findErr = findErr
  constructor(
    injector: Injector,
    private fb: FormBuilder,
    private myProfileService: MyProfileService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _dialogRef: MatDialogRef<PersonalAtributeComponent>,
    private _permissionChecker: PermissionCheckerService,
    private session: AppSessionService,
    private translate: TranslateService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.isSale = this._permissionChecker.isGranted('Pages.EditAsSales.Employee');
    this.isUser = this.data.isUser;
    this.buildForm();
    this.body = this.data;
    this.listData = this.body.listPersonalAtribute.length === 0 ? [] : this.body.listPersonalAtribute;
    if (this.data.item === undefined) {
      this.title = 'child1.personalDialog.create';
      this.atributeForm.get('atribute').setValue(null);
    } else {
      for (let i = 0; i <= this.listData.length; i++) {
        if (this.data.item === this.listData[i]) {
          this.foundItem = i;
        }
      }
      this.atributeForm.get('atribute').setValue(this.data.item);
      this.title = 'child1.personalDialog.edit';
    }
  }

  buildForm() {
    this.atributeForm = this.fb.group({
      atribute: [''],
    });
  }
  saveAtribute() {
    if (this.title === 'Edit atribute') {
      for (let i = 0; i <= this.listData.length; i++) {
        if (this.foundItem === i) {
          this.listData[i] = this.atributeForm.controls.atribute.value;
        }
      }
    } else {
      this.listData.push(this.atributeForm.controls.atribute.value);
    }
    const data = {
      personalAttributes: this.listData
    };
    if (this.isSale && !this.isUser) {
      this._dialogRef.close(data);
    } else {
      this.myProfileService.updatePersonalAttribute(data).subscribe(res => {
        this._dialogRef.close(res);
      }, err => {
        this.notify.error(this.translate.instant(this.findErr(err)));
      });
    }
  }
}

