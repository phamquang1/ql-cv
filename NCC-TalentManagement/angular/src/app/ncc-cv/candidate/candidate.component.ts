import { Component, OnInit, Injector } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BranchDto } from '@app/services/model/common-dto';
import { Router } from '@angular/router';
import { CommonService } from '@app/services/common.service';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { NamDuan, Thang, Degree } from '@app/services/model/year-plan';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CandidateService } from '@app/services/candidate-service';
import { finalize } from 'rxjs/operators';
import { CandidateDto } from '../../services/model/candidate-dto';
import { StatusDto } from '../../services/model/common-dto';

@Component({
  selector: 'app-candidate',
  templateUrl: './candidate.component.html',
  styleUrls: ['./candidate.component.css'],
  animations: [appModuleAnimation()]
})
export class CandidateComponent extends PagedListingComponentBase<any> {
  status: StatusDto[] = [];
  searchForm: FormGroup;
  listCandidate: CandidateDto [] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;
  listNam = NamDuan.DanhSach ;
  listThang = Thang.DanhSach;
  listTrinhdo = Degree.DanhSach;
  branch: BranchDto[] = [];
  candidateNumber = 0;
  pageSizeType = 10;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private injector: Injector,
    private commonService: CommonService,
    private candidateService: CandidateService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.buildForm();
    this.getAllBranch();
    this.GetCBBStatusCandidate();
    this.refresh();
  }
  buildForm () {
    this.searchForm = this.fb.group({
      skill: [''],
      branch: [''],
      collectedMonth: [''],
      collectedYear: [''],
      status: [''],
    });
  }

  // thêm mới candidate
  createCandidate() {
    this.router.navigate(['app/main/candidate/create-candidate']);
  }
  // chi tiết candidate
  detailCandidate(id) {
    this.router.navigate(['app/main/candidate/detail-candidate', id]);
  }

  list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    let data = {
      Search : this.keyword,
      Skill : this.searchForm.get('skill').value,
      BranchId : this.searchForm.get('branch').value,
      Status : this.searchForm.get('status').value,
      MonthReceived : this.searchForm.get('collectedMonth').value,
      YearReceived : this.searchForm.get('collectedYear').value,
      MaxResultCount : request.maxResultCount,
      SkipCount : request.skipCount
    }
      this.candidateService.GetAllCandidatePaging(data.Search, data.Skill, data.BranchId, data.Status, data.MonthReceived,
                                                  data.YearReceived, data.MaxResultCount, data.SkipCount).
      pipe(
        finalize(() => {
          finishedCallback();
        })
      ).subscribe(res => {
        if (res) {
          this.listCandidate = res.result.items.map(el => {
            return {
              ...el,
              statusName: this.getNameById(this.status, el.status),
            }
          });
          this.candidateNumber = res.result.totalCount;
          this.showPaging(res.result, pageNumber);
        }
      });
  }

  clearFilters(): void {
    this.keyword = '';
    this.searchForm.get('skill').setValue('');
    this.searchForm.get('branch').setValue('');
    this.searchForm.get('status').setValue('');
    this.searchForm.get('collectedMonth').setValue('');
    this.searchForm.get('collectedYear').setValue('');
    this.getDataPage(1);
  }
  changePageSize() {
    if (this.pageSizeType > this.candidateNumber) {
      this.pageNumber = 1;
    }
    this.pageSize = this.pageSizeType;
    this.refresh();
  }
  // lấy tên status
  getNameById(list: any[], id: number): string {
    const founded = list.find(el => el.key === id);
    return founded ? founded.valueVN : '';
  }

  // lấy branch
  getAllBranch() {
    this.commonService.GetAllBranch().subscribe(res => {
      this.branch = res.result;
    });
  }
  // lấy trạng thái candidate
  GetCBBStatusCandidate() {
    this.commonService.GetCBBStatusCandidate().subscribe(res => {
      this.status = res.result;
    });
  }
  getAllPosition() {}

  delete(employee: CandidateDto): void {}
}