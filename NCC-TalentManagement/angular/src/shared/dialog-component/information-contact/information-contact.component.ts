import { Component, OnInit ,Inject, Injector} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MyProfileService} from '../../../app/services/my-profile-service';
import {CommonService} from '../../../app/services/common.service';
import { forEach } from 'lodash';
import { InformDto, IObjectFile } from '@app/services/model/my-profile-dto';
import {PositionDto, BranchDto} from '../../../app/services/model/common-dto';
import { AppConsts } from '../../AppConsts';
import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/session/app-session.service';
import { TranslateService } from '@ngx-translate/core';
import { AppComponentBase } from '@shared/app-component-base';
import { findErr } from '@app/services/model/year-plan';

@Component({
  selector: 'app-information-contact',
  templateUrl: './information-contact.component.html',
  styleUrls: ['./information-contact.component.css']
})
export class InformationContactComponent extends AppComponentBase implements OnInit {
  inform: InformDto;
  imagePath: any ;
  img = {
    buffer: null,
    file: null,
    path: '',
  } as IObjectFile;
  banner: IObjectFile[] = [];
  surname: string;
  name: string;
  file: File = null;
  showImg = true;
  position: PositionDto[] = [];
  branch: BranchDto[] = [];
  urlImg = AppConsts.remoteServiceBaseUrl;
  isSale: boolean;
  isUser: boolean;
  isEmployee = false;
  id: number;
  public findErr = findErr
  constructor(
    injector: Injector,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private myProfileService: MyProfileService,
    private commonService: CommonService,
    private _dialogRef: MatDialogRef<InformationContactComponent>,
    private _permissionChecker: PermissionCheckerService,
    private session: AppSessionService,
    private translate: TranslateService
  ) {
    super(injector);
   }

  ngOnInit(): void {
    this.isSale = this._permissionChecker.isGranted('Pages.EditAsSales.Employee');
    this.inform = this.data;
    this.isUser = this.session.userId === Number(this.data.userId);
    this.GetAllPositionType();
    this.GetAllBranch();
  }
  //
  filterPosition() {
    this.position.forEach(item => {
      if ( this.inform.currentPosition === item.name) {
        this.inform.currentPosition = item.id;
      }
    });
  }
  filterBranch(){
    forEach(this.branch, item => {
      if (this.inform.branch === item.name) {
        this.inform.branch = item.id;
      }
    });
  }
  changeInmage(event: any): void {
    this.showImg = false;
    let reader = new FileReader();
    this.img.file = event.target.files[0];
    reader.readAsDataURL(this.img.file);
    reader.onload = (_event) => {
      this.img.buffer = reader.result;
    }
  }
  deleteImage() {
    this.img.file = null;
    this.inform.imgPath = '';
  }
  GetAllPositionType() {
    this.commonService.GetAllPositionType().subscribe(res=>{
      this.inform = this.data;
      this.position = res.result;
      this.filterPosition();
    });
  }
  GetAllBranch() {
    this.commonService.GetAllBranch().subscribe(res => {
      this.branch = res.result;
      this.filterBranch();
    })
  }
  submitInfoContact() {
    const request = new FormData();
    //validate
    if(!this.inform.name){
      this.notify.error(this.translate.instant('child1.errDialog.err6'));
      return ;
    }
    if(!this.inform.surname){
      this.notify.error(this.translate.instant('child1.errDialog.err7'));
      return ;
    }
    if(!this.inform.phoneNumber){
      this.notify.error(this.translate.instant('child1.errDialog.err8'));
      return ;
    }
    if(!this.inform.emailAddressInCV){
      this.notify.error(this.translate.instant('child1.errDialog.err9'));
      return ;
    }
    //
    if (this.img.file) {
      request.append('File', this.img.file);
    } else {
      request.append('Path', this.inform.imgPath);
      request.append('File', '');
    }
    if (this.inform.name) {
      request.append('Name', this.inform.name);
    }
    if (this.inform.surname) {
      request.append('Surname', this.inform.surname);
    }
    if (this.inform.phoneNumber) {
      request.append('PhoneNumber', this.inform.phoneNumber);
    }
    if (this.inform.emailAddressInCV) {
      request.append('EmailAddressInCV', this.inform.emailAddressInCV);
    }
    if (this.inform.currentPosition) {
      request.append('CurrentPositionId', this.inform.currentPosition);
    }
    if (this.inform.userId) {
      request.append('UserId', this.inform.userId);
    }
    if (this.inform.address) {
      request.append('Address', this.inform.address);
    }
    if (this.inform.branch){
      request.append('BranchId', this.inform.branch);
    }
    if(this.isSale && !this.isUser){
      this._dialogRef.close(this.inform);
    } else {
      this.myProfileService.SaveUserGeneralInfo(request).subscribe(res => {
        this._dialogRef.close(res);
      }, err => {
        this.notify.error(this.translate.instant(this.findErr(err)));
      });
    }
  }
}



