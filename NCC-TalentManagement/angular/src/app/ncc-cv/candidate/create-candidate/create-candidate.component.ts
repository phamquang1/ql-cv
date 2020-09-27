import { Component, OnInit, Injector, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonService } from '@app/services/common.service';
import { BranchDto, PositionDto , InterviewDto, SkillCandidateDto} from '@app/services/model/common-dto';
import { Degree, findErr } from '@app/services/model/year-plan';
import { Router, ActivatedRoute } from '@angular/router';
import { CandidateService} from 'app/services/candidate-service';
import { AppComponentBase } from '@shared/app-component-base';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { StatusDto } from '../../../services/model/common-dto';
import { AppConsts } from '@shared/AppConsts';
import { TranslateService } from '@ngx-translate/core';
const typeofFile = {
  img: '0' ,
  file: '1',
}
@Component({
  selector: 'app-create-candidate',
  templateUrl: './create-candidate.component.html',
  styleUrls: ['./create-candidate.component.css'],
})

export class CreateCandidateComponent extends AppComponentBase implements OnInit {
  createCandidateForm: FormGroup;
  branch: BranchDto[] = [];
  position: PositionDto[] = [];
  listInterview: InterviewDto[] = [];
  status: StatusDto[] = [];
  listSkillForCandidates: SkillCandidateDto[] = [];
  listPresent: InterviewDto[] = [];
  listOldCv: InterviewDto[] = [];
  listTrinhdo = Degree.DanhSach;
  fileName = '';
  filepath = '';
  typeforFile = typeofFile;
  panelOpenState = false;
  filesImgDisplay: imgPath [] = [] ;
  urlImg = AppConsts.remoteServiceBaseUrl;
  filesImgTransform = [];
  filesImgName = [];
  searchText: string;
  SkipCount: number;
  MaxResultCount: number;
  label = 'result for search';
  isSave = false;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  listInterViewFilter = [];
  listSkillFilter = [];
  @ViewChild('fruitInput') fruitInput: ElementRef<HTMLInputElement>;
  @ViewChild('fruitInput1') fruitInput1: ElementRef<HTMLInputElement>;
  id: number;
  isEdit = false;
  isDisable: boolean;
  listCombobox = ['isInterview', 'isSkill', 'isPresent', 'isOldCV' ];
  title: string;
  public findErr = findErr;
  constructor(
    injector: Injector,
    private fb: FormBuilder,
    private commonService: CommonService,
    private router: Router,
    private route: ActivatedRoute,
    private candidateService: CandidateService,
    private translate: TranslateService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'] ? this.route.snapshot.params['id'] : null;
    this.title = this.id === null ? 'child2.createOrdetail.titlecreate' : 'child2.createOrdetail.title';
    this.buildForm();
    this.listCombobox.forEach(item => {
      this.getInterviewCandidate('', item);
    });
    if ( this.id !== null) {
      this.isEdit = true;
      this.isDisable = true;
      this.getDetailCandidate();
 }
    this.getAllPosition();
    this.getAllBranch();
    this.GetCBBStatusCandidate();
  }
  editCV() {
     this.isDisable = false;
     this.isEdit = false;
  }
  deleteCV() {
    this.candidateService.deleteCandidate(this.id).subscribe(res => {
      this.notify.success(this.translate.instant('message.deleteSuccess'));
      this.router.navigate(['app/main/candidate']);
    });
  }
  getDetailCandidate() {
    this.candidateService.getCandidateInfoById(this.id).subscribe( res => {
      this.createCandidateForm.patchValue({
        ...res.result,
        positionId: res.result.position.id,
        branchId: res.result.branch.id,
        presenteName: res.result.presenter == null ? null : res.result.presenter.name,
        oldCVName: this.findName(this.listOldCv, res.result.oldCVId)
      });
      res.result.interviewCandidates.forEach(item => {
        if ( item) {
          this.listInterViewFilter.push(item.name);
        }
      });
      res.result.cvSkills.forEach(item => {
        if ( item) {
          this.listSkillFilter.push(item.skillName);
        }
      });
      this.filepath = res.result.attachmentPath;
      this.fileName = 'CV_' + res.result.fullName;
      this.filesImgName = res.result.attachments;
      res.result.attachments.forEach(item => {
        this.filesImgDisplay.push({
          buffer : this.urlImg + '/' + item,
          path : item
        } as imgPath);
      });
      res.result.attachments.forEach(element => {
        this.filesImgTransform.push(element)
      });
    });
  }

  buildForm () {
    this.createCandidateForm = this.fb.group({
      fullName: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', Validators.email],
      positionId: ['', Validators.required],
      branchId: ['', Validators.required],
      receiveTime: [''],
      interviewTime: [''],
      startWorkingTime: [''],
      status: [''],
      attachmentPatch: [''],
      degreeType: [''],
      oldCVId: [''],
      source: [''],
      interviewerId: [''],
      interviewerName: [''],
      skillId: [''],
      skillName: [''],
      workExperience: [''],
      presenterId: [''],
      presenteName: [''],
      oldCVName: ['']
    });
  }
  //
  getAllPosition() {
    this.commonService.GetAllPositionType().subscribe(res => {
      this.position = res.result;
    });
  }
  getAllBranch() {
    this.commonService.GetAllBranch().subscribe(res => {
      this.branch = res.result;
    });
  }
  GetCBBStatusCandidate() {
    this.commonService.GetCBBStatusCandidate().subscribe(res => {
      this.status = res.result;
    });
  }
  getInterviewCandidate(searchText, status) {
  this.SkipCount  = 0;
  this.MaxResultCount = 200;
  switch (status) {
    case 'isInterview':
      this.commonService.getCBBInterviewer(searchText, this.SkipCount, this.MaxResultCount).subscribe( res => {
        this.listInterview = res.result.items;
      });
      break;
    case 'isSkill':
      this.commonService.getCBBSkillForCandidate(searchText, this.SkipCount, this.MaxResultCount).subscribe( res => {
        this.listSkillForCandidates = res.result.items;
      });
      break;
    case 'isPresent':
      this.commonService.getCBBPresenter(searchText, this.SkipCount, this.MaxResultCount).subscribe( res => {
        this.listPresent = res.result.items;
      });
      break;
    case 'isOldCV':
      this.commonService.getCBBOldCVId(searchText, this.SkipCount, this.MaxResultCount).subscribe( res => {
        this.listOldCv = res.result.items.map(el => {
          return {
               ...el,
               id: el.id,
               name: el.email
          }
        });
      });
      break;
    }
  }

  onKey(value, status) {
    this.getInterviewCandidate(value, status);
  }

  remove(item: string, status): void {
    switch (status) {
      case 'isInterview':
        const index = this.listInterViewFilter.indexOf(item);
        if (index >= 0) { this.listInterViewFilter.splice(index, 1); }
        break;
      case 'isSkill':
        const index1 = this.listSkillFilter.indexOf(item);
        if (index1 >= 0) { this.listSkillFilter.splice(index1, 1); }
        break;
    }

  }
  selected(event: MatAutocompleteSelectedEvent, status): void {
    switch (status) {
      case 'isInterview':
        this.listInterViewFilter.push(event.option.viewValue);
        this.fruitInput.nativeElement.value = '';
        this.createCandidateForm.get('interviewerName').setValue(null);
        break;
      case 'isSkill':
        this.listSkillFilter.push(event.option.viewValue);
        this.fruitInput1.nativeElement.value = '';
        this.createCandidateForm.get('skillName').setValue(null);
        break;
    }
  }

  saveCV() {
    this.isSave = true;
    let listcvSkill = [];
    let listInter = [];
    if (this.listSkillFilter !== [] ) {
      listcvSkill = this.listSkillFilter.map( el => {
        return { skillId: this.findId(this.listSkillForCandidates, el) }
      });
    }
    if (this.listInterViewFilter !== [] ) {
    listInter = this.listInterViewFilter.map( el => {
      return { interviewerId: this.findId(this.listInterview, el)}
      });
    }
    const formData = this.createCandidateForm.value;
    if ((formData.interviewTime !== null) && (new Date(formData.interviewTime).getTime() - new Date(formData.receiveTime).getTime()) < 0) {
        this.notify.error(this.translate.instant('child2.createOrdetail.error1'));
      return;
    }
    if ((formData.startWorkingTime !== null) &&
        (new Date(formData.startWorkingTime).getTime() - new Date(formData.interviewTime).getTime()) < 0) {
        this.notify.error(this.translate.instant('child2.createOrdetail.error2'));
      return;
    }
    if ((formData.startWorkingTime !== null) &&
        (new Date(formData.startWorkingTime).getTime() - new Date(formData.receiveTime).getTime()) < 0) {
        this.notify.error(this.translate.instant('child2.createOrdetail.error3'));
      return;
    }
    if ( this.fileName === '') {
      return;
    }
    if (this.createCandidateForm.invalid) {
      this.notify.error(this.translate.instant('child2.createOrdetail.error4'));
      return;
    }
    let data = {
      id: this.id == null ? 0 : this.id,
      fullName: formData.fullName,
      phone: formData.phone,
      email: formData.email,
      positionId: formData.positionId,
      branchId: formData.branchId,
      receiveTime: formData.receiveTime,
      interviewTime: formData.interviewTime,
      startWorkingTime: formData.startWorkingTime,
      workExperience: formData.workExperience,
      status: formData.status,
      degreeType: formData.degreeType,
      source: formData.source,
      oldCVId: this.findId(this.listOldCv, formData.oldCVName),
      presenterId: this.findId(this.listPresent, formData.presenteName),
      attachmentPatch: this.filepath,
      attachments: this.filesImgTransform,
      cvSkills: listcvSkill,
      interviewCandidates: listInter,
     }
      this.candidateService.insertOrUpdateCandidate(data).subscribe( res => {
        if (res) {
          this.notify.success(this.translate.instant('message.createSuccess'));
          this.router.navigate(['app/main/candidate']);
        }
        },err =>{
          this.notify.error(this.translate.instant(this.findErr(err)));
        });
  }

  findId(list: any[], name: string) {
    const foundId = list.find(el => el.name === name);
    return foundId ? foundId.id : '';
  }
  findName(list: any[], id: number) {
    const found = list.find(el => el.id === id);
    return found ? found.email : '';
  }


  exit() {
    if ( this.filepath !== '') {
      this.deleteFile();
    }
    if (this.filesImgName.length !== 0) {
     for ( let i = 0; i <= this.filesImgName.length; i ++) {
      this.deleteFileImg(i);
     }
    }
    this.router.navigate(['app/main/candidate']);
  }
  // file
  getFile(file: any) {
    this.fileName = file.target.files[0].name;
    let files = file.target.files[0];
    let type = typeofFile.file ;
    const request = new FormData();
    if (files) {
      request.append( 'files', files);
    } if (type) {
      request.append('typeUpload', type);
    }
    const urlImg = AppConsts.remoteServiceBaseUrl;
    this.candidateService.uploadAttachments(request).subscribe(res => {
     this.filepath =  res.result.paths[0];
    });
  }
  deleteFile() {
    let paths = [];
    paths.push( this.filepath);
    let data  = {
      typeFile: Number(typeofFile.file),
      paths: paths
    }
    this.candidateService.cancelUploadFile(data).subscribe( res => {
    });
    this.fileName = '';
    this.filepath = '';
  }
  // image
  changeImage(event: any){
    if (event.target.files && event.target.files.length > 0) {
      for (let i = 0; i < event.target.files.length; ++i) {
        this.filesImgName.push(event.target.files[i].name);
        let image = {} as imgPath;
         image.path = event.target.files[i];
        const reader = new FileReader();
        reader.onload = e => image.buffer = reader.result;
        reader.readAsDataURL(image.path);
        this.filesImgDisplay.push(image);
      }
    }
    const request = new FormData();
    let type = typeofFile.img ;
    this.filesImgDisplay.forEach(item => {
      if (item.path) {
        request.append('files', item.path);
      }
    });
    if (type) {
      request.append('typeUpload', type);
    }
    this.candidateService.uploadAttachments(request).subscribe(res => {
      this.filesImgTransform = this.filesImgTransform.concat(res.result.paths)
     });
  }
  deleteFileImg(i: number) {
    let pathImg = {
      typeFile: Number(typeofFile.img),
      paths: this.filesImgTransform
    }
    this.filesImgName.splice(i, 1);
    this.filesImgTransform.splice(i, 1);
    this.filesImgDisplay.splice(i, 1);
    this.candidateService.cancelUploadFile(pathImg).subscribe( res => {
    }, err => {

    });
  }
}

export class imgPath {
  buffer: any;
  path: File;
}

