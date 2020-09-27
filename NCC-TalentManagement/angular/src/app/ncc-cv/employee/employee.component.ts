import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { FormBuilder, FormGroup } from '@angular/forms';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Router } from '@angular/router';
import { CommonService } from '@app/services/common.service';
import { EmployeeService } from '@app/services/employee.service';
import { BranchDto, PositionDto } from '@app/services/model/common-dto';
import { finalize } from 'rxjs/operators';
import { EmployeeDto } from '../../services/model/employee-dto';
class PagedEmployeeRequestDto extends PagedRequestDto {
  name?: string;
  positionId?: string;
  branchId?: string;
}
@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
  animations: [appModuleAnimation()]
})
export class EmployeeComponent extends PagedListingComponentBase<EmployeeDto> {
  searchForm: FormGroup;
  listEmployee: EmployeeDto[] = [];
  name = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;
  branch: BranchDto[] = [];
  position: PositionDto[] = [];
  positionId: number;
  branchId: number;
  employeeNumber = 0;
  pageSizeType = 10;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private injector: Injector,
    private commonService: CommonService,
    private employeeService: EmployeeService,
  ) {
    super(injector);
  }

  ngOnInit() {
    this.getAllBranch();
    this.getAllPosition();
    super.ngOnInit();
  }

   list(
    request: PagedEmployeeRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    const data = {
      name: this.name,
      positionId: this.positionId,
      branchId: this.branchId,
      maxResultCount: request.maxResultCount,
      skipCount: request.skipCount,
    }
    this.employeeService.GetAllEmployeePaging(data)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      ).subscribe(res => {
        if (res) {
          this.isTableLoading = true;
          this.listEmployee = res.result.items;
          this.employeeNumber = res.result.totalCount;
          this.showPaging(res.result, pageNumber);
          this.isTableLoading =  false;
        }
      }, err => {
        this.isTableLoading = false;
      });
    }
  changePageSize() {
    if (this.pageSizeType > this.employeeNumber) {
      this.pageNumber = 1;
    }
    this.pageSize = this.pageSizeType;
    this.refresh();
  }
  findId(list: any[], id: number) {
    const foundId = list.find(el => el.id === id);
    return foundId ? foundId.id : '';
  }

  getNameById(list: any[], id: number): string {
    const founded = list.find(el => el.id === id);
    return founded ? founded.name : '';
  }

  clearFilters(): void {
    this.name = '';
    this.positionId = null;
    this.branchId = null;
    this.getDataPage(1);
  }
  getAllBranch() {
    this.isTableLoading  = true;
    this.commonService.GetAllBranch().subscribe(res => {
      this.branch = res.result;
      this.isTableLoading = false;
      // this.filterBranch();
    });
  }
  getAllPosition() {
    this.isTableLoading = true;
    this.commonService.GetAllPositionType().subscribe(res => {
      this.position = res.result;
      this.isTableLoading = false;
    });
  }
  delete(): void { }
  detailEmployee(id) {
    this.router.navigate(['app/main/employee/detail-employee', id]);
  }
}