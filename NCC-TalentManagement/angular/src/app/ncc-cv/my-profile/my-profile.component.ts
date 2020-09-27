import { Component, OnInit, Injector, Input, AfterContentChecked } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { InformationContactComponent } from '../../../shared/dialog-component/information-contact/information-contact.component';
import { EducationComponent } from '../../../shared/dialog-component/education/education.component';
import { AppComponentBase } from '@shared/app-component-base';
import { MyProfileService } from '../../services/my-profile-service';
import { InformDto, EducationDto, WorkingExperienceDto, EducationOderDto, TechnicalExpertise } from '../../services/model/my-profile-dto';
import { AppSessionService } from '@shared/session/app-session.service';
import { Degree } from '../../services/model/year-plan';
import { TechnicalExpertisesComponent } from '@shared/dialog-component/technical-expertises/technical-expertises.component';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { PersonalAtributeComponent } from '@shared/dialog-component/personal-atribute/personal-atribute.component';
import { WorkingExperiencesComponent } from '@shared/dialog-component/working-experiences/working-experiences.component';
import { AppConsts } from '../../../shared/AppConsts';
import { PermissionCheckerService } from 'abp-ng2-module';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CommonService } from '@app/services/common.service';
import { PositionDto } from '@app/services/model/common-dto';
import { DialogExportComponent } from './dialog-export/dialog-export.component';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
  animations: [appModuleAnimation()]
})

export class MyProfileComponent extends AppComponentBase implements OnInit, AfterContentChecked {
  employeeName: string;
  inform: InformDto;
  listskill: TechnicalExpertise;
  groupSkillNumber = 0;
  image: string;
  urlImg = AppConsts.remoteServiceBaseUrl;
  fullName: string;
  surname: string;
  name: string;
  listEducation: EducationDto[] = [];
  dataForEdit: any;
  workingExperience: WorkingExperienceDto[] = [];
  listDegree = Degree.DanhSach;
  isUser: boolean;
  listPersonalAtribute: any;
  isSale = false;
  editAsSale = false;
  @Input() id: number;
  @Input() isEmployee = false;
  filePathofExport: string;
  fileNameExport: string;
  position: PositionDto[] = [];
  constructor(
    injector: Injector,
    private dialog: MatDialog,
    private myProfileService: MyProfileService,
    private session: AppSessionService,
    private _permissionChecker: PermissionCheckerService,
    private commonService: CommonService,
    private router: Router,
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
    this.isUser = this.session.userId === Number(this.id);
    this.isSale = this._permissionChecker.isGranted('Pages.EditAsSales.Employee');
    this.getInformUser();
    this.getTechnicalExpertise();
    this.getEducationInfo();
    this.getUserWorkingExperience();
    this.getPersonalAttribute();
    this.GetAllPositionType();
  }
  // dialog chọn định dạng kết xuất
  showDialogExport() {
    if (this.isSale && !this.isUser) {
      let item = [{
        typeOffile: '',
        isHiddenYear: false,
        employeeInfo: this.inform,
        educationBackGround: this.listEducation,
        technicalExpertises: this.listskill,
        personalAttributes: { personalAttributes: this.listPersonalAtribute },
        workingExperiences: this.workingExperience,
      },
      this.isUser
      ]
      const dialogRef = this.dialog.open(DialogExportComponent, {
        data: item
      });
      dialogRef.afterClosed().subscribe(res => {
      });
    } else {
      const userId = this.id;
      const item = [{ userId }, this.isUser];
      const dialogRef = this.dialog.open(DialogExportComponent, {
        data: item
      });
      dialogRef.afterClosed().subscribe(res => {
      });
    }

  }
  // Lấy thông tin nhân viên
  getInformUser() {
    this.myProfileService.getUserGeneralInfo(this.id).subscribe(res => {
      this.inform = res.result;
      this.employeeName = res.result.surname + ' ' + res.result.name;
      this.surname = res.result.surname;
      this.name = res.result.name;
    });
  }
  showDialogCreatEdit(inform: InformDto) {
    let item = {
      name: inform.name,
      surname: inform.surname,
      phoneNumber: inform.phoneNumber,
      emailAddressInCV: inform.emailAddressInCV,
      imgPath: inform.imgPath,
      currentPosition: inform.currentPosition,
      userId: inform.userId,
      address: inform.address,
      branch: inform.branch,
    }
    const dialogRef = this.dialog.open(InformationContactComponent, {
      disableClose: true,
      data: item,
    });
    dialogRef.afterClosed().subscribe(res => {
      if (this.isSale && !this.isUser) {
        this.inform = res;
        this.inform.currentPosition = this.filterPosition(this.position, res.currentPosition)
        this.employeeName = res.surname + ' ' + res.name;
        this.surname = res.surname;
        this.name = res.name;
      } else {
        this.getInformUser();
      }
    });
  }
  // end infor

  // Lấy thông tin, thêm sửa xóa  kinh nghiệm công việc
  getUserWorkingExperience() {
    this.myProfileService.getUserWorkingExperience(this.id).subscribe(res => {
      this.workingExperience = res.result;
    });
  }
  editWorkingExp(workingEpx: WorkingExperienceDto) {
    this.showDialogCreateEditWorkingExp(workingEpx);
  }
  deleteWorkingExp(workingEpx: WorkingExperienceDto) {
    abp.message.confirm(
      undefined,
      this.translate.instant('message.delete'),
      (result: boolean) => {
        if (result) {
          if (this.isSale && !this.isUser) {
            const index = this.workingExperience.indexOf(workingEpx);
            this.workingExperience.splice(index, 1);
          } else {
            this.myProfileService.DeleteWorkingExperience(workingEpx.id).subscribe(res => {
              if (res) {
                this.notify.success(this.translate.instant('message.deleteSuccess'));
                this.getUserWorkingExperience();
              }
            }, err => {
            });
          }
        }
      }
    );
  }
  createWorkingExp() {
    let workingExp = {
      id: 0,
      userId: this.id
    } as WorkingExperienceDto;
    this.showDialogCreateEditWorkingExp(workingExp);
  }

  showDialogCreateEditWorkingExp(workingEpx: WorkingExperienceDto) {
    let item = [{
      endTime: workingEpx.endTime,
      id: workingEpx.id,
      order: workingEpx.order,
      position: workingEpx.position,
      projectDescription: workingEpx.projectDescription,
      projectName: workingEpx.projectName,
      responsibility: workingEpx.responsibility,
      startTime: workingEpx.startTime,
      technologies: workingEpx.technologies,
      userId: workingEpx.userId
    },
    this.isUser
    ];
    const dialogRef = this.dialog.open(WorkingExperiencesComponent, {
      data: item
    });
    let index = this.workingExperience.indexOf(workingEpx);
    dialogRef.afterClosed().subscribe(res => {
      if (this.isSale && !this.isUser) {
        if (res) {
          if (!workingEpx.projectName) {
            this.workingExperience.unshift(res);
          } else {
            this.workingExperience[index] = res;
          }
        } else {

        }
      } else {
        this.getUserWorkingExperience();
      }
    });
  }
  dropWorking(event: CdkDragDrop<string[]>) {
    if ((!this.isEmployee && this.isUser) || (this.isEmployee && this.isUser)) {
      let workingExp = [];
      moveItemInArray(this.workingExperience, event.previousIndex, event.currentIndex);
      for (let i = 0; i < this.workingExperience.length; i++) {
        workingExp.push({
          id: this.workingExperience[i].id,
          order: i
        });
      }
      this.myProfileService.UpdateOrderWorkingExperience(workingExp).subscribe(res => {
        if (res) {
          this.getUserWorkingExperience();
        }
      });
    } else {
      return;
    }

  }
  // end working experience

  // Lấy thông tin. thêm sửa xóa thông tin giáo dục
  getEducationInfo() {
    this.myProfileService.getEducationInfo(this.id).subscribe(res => {
      this.listEducation = res.result.map(el => {
        return {
          ...el,
          degreeType: this.viewDegree(el.degreeType),
        }
      });
    });
  }
  viewDegree(degreeType) {
    switch (degreeType) {
      case 0:
        return 'HighSchool';
      case 1:
        return 'Bachelor';
      case 2:
        return 'Master';
      case 3:
        return 'PostDoctor';
      case 4:
        return 'Certificate';
    }

  }
  createEducation() {
    let education = {
      id: 0,
      cvemployeeId: this.id
    } as EducationDto;
    this.dialogCreateEditEducation(education);
  }
  editEducation(item) {
    this.dialogCreateEditEducation(item);
  }
  deleteEducation(item: EducationDto) {
    abp.message.confirm(
      undefined,
      this.translate.instant('message.delete'),
      (result: boolean) => {
        if (result) {
          if (this.isSale && !this.isUser) {
            let index = this.listEducation.indexOf(item)
            this.listEducation.splice(index, 1)
          } else {
            this.myProfileService.DeleteEducation(item.id).subscribe(res => {
              this.notify.success(this.translate.instant('message.deleteSuccess'));
              this.getEducationInfo();
            }, err => {

            });
          }
        }
      }
    );
  }
  dialogCreateEditEducation(education: EducationDto) {
    let item = [
      {
        cvcandidateId: education.cvcandidateId,
        cvemployeeId: education.cvemployeeId,
        degreeType: education.degreeType,
        description: education.description,
        major: education.major,
        schoolOrCenterName: education.schoolOrCenterName,
        startYear: education.startYear,
        endYear: education.endYear,
        order: education.order,
        id: education.id
      },
      this.isUser
    ]
    const dialogRef = this.dialog.open(EducationComponent, {
      data: item
    });
    let index = this.listEducation.indexOf(education);
    dialogRef.afterClosed().subscribe(res => {
      if (this.isSale && !this.isUser) {
        if (res) {
          if (!education.schoolOrCenterName) {
            this.listEducation.push(res);
          } else {
            this.listEducation[index] = res;
            this.listEducation[index].degreeType = this.viewDegree(res.degreeType);
          }
        } else {
        }
      } else {
        this.getEducationInfo()
      }
    });
  }
  /// drap drop
  dropEducation(event: CdkDragDrop<string[]>) {
    if ((!this.isEmployee && this.isUser) || (this.isEmployee && this.isUser)) {
      let educationOder = [];
      moveItemInArray(this.listEducation, event.previousIndex, event.currentIndex);
      for (let i = 0; i < this.listEducation.length; i++) {
        educationOder.push({
          id: this.listEducation[i].id,
          order: i
        } as EducationOderDto)
      }
      this.myProfileService.UpdateOrderEducation(educationOder).subscribe(res => {
        if (res) {
          this.getEducationInfo();
        }
      });
    } else {
      return;
    }

  }
  // Lấy thông tin thêm sửa xóa thông tin atribute
  getPersonalAttribute() {
    this.myProfileService.getPersonalAttribute(this.id).subscribe(res => {
      if (res.result === null) {
        this.listPersonalAtribute = [];
      }
      this.listPersonalAtribute = res.result.personalAttributes;
    });
  }
  createAtribute(listPersonalAtribute): void {
    this.showDialogCreateEditAtribute(listPersonalAtribute);
  }

  editAtribute(listPersonalAtribute, item): void {
    this.showDialogCreateEditAtribute(listPersonalAtribute, item);
  }

  deleteAtribute(index) {
    abp.message.confirm(
      undefined,
      this.translate.instant('message.delete'),
      (result: boolean) => {
        if (result) {
          if (this.isSale && !this.isUser) {
            this.listPersonalAtribute.splice(index, 1);
          } else {
            this.listPersonalAtribute.splice(index, 1);
            const data = { personalAttributes: this.listPersonalAtribute };
            this.myProfileService.updatePersonalAttribute(data).subscribe(res => {
              if (res) {
                this.notify.success(this.translate.instant('message.deleteSuccess'));
                this.getPersonalAttribute();
              }
            });
          }
        }
      })
  }

  showDialogCreateEditAtribute(listPersonalAtribute, item?) {
    const isUser = this.isUser;
    const body = {
      listPersonalAtribute,
      item,
      isUser
    };
    const dialogRef = this.dialog.open(PersonalAtributeComponent, {
      height: '430px',
      width: '500px',
      data: body
    });
    dialogRef.afterClosed().subscribe(res => {
      if (this.isSale && !this.isUser) {
        if (res) { this.listPersonalAtribute = res.personalAttributes; }
      } else {
        if (res) {
          this.getPersonalAttribute();
        }
      }

    });
  }
  // end atribute
  // Lấy thông tin , thêm sửa xóa edit technical
  getTechnicalExpertise() {
    this.myProfileService.getTechnicalExpertise(this.id).subscribe(res => {
      this.listskill = res.result;
      this.groupSkillNumber = this.listskill.groupSkills.length;
    });
  }
  createTechnical(): void {
    const listskill = {
      userId: this.id,
      groupSkills: []
    } as TechnicalExpertise;
    this.showDialogCreateEditTechnical(listskill);
  }
  editTechnical(listskill: TechnicalExpertise): void {
    this.showDialogCreateEditTechnical(listskill);
  }
  showDialogCreateEditTechnical(listskill: TechnicalExpertise) {
    let item = [
      listskill,
      this.isUser
    ]
    const dialogRef = this.dialog.open(TechnicalExpertisesComponent, {
      data: item
    });
    dialogRef.afterClosed().subscribe(res => {
      if (this.isSale && !this.isUser) {
        if (res) {
          this.listskill = res;
          this.groupSkillNumber = this.listskill.groupSkills.length;
        } else {

        }
      } else {
        this.getTechnicalExpertise();
      }
    });
  }
  // end dialog techincal
  //
  xuongDong(value: string) {
    if (value) {
      return value.split('\n').join('<br>');
    }
  }
  EditAsSale() {
    this.editAsSale = !this.editAsSale;
  }
  GetAllPositionType() {
    this.commonService.GetAllPositionType().subscribe(res => {
      this.position = res.result;
    });
  }
  filterPosition(position: PositionDto[], type: number) {
    const founded = position.find(el => el.id === type);
    return founded ? founded.name : '';
  }
  exitEmployee() {
    this.router.navigate(['app/main/employee']);
  }
}


