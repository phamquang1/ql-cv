import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';
import { SharedModule } from '@shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatInputModule } from '@angular/material/input';
import { DetailEmployeeComponent } from './detail-employee/detail-employee.component';
import { MyProfileModule } from '../my-profile/my-profile.module';
 import {TranslateModule, TranslateLoader} from '@ngx-translate/core';


@NgModule({
  declarations: [EmployeeComponent, DetailEmployeeComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    EmployeeRoutingModule,
    SharedModule,
    NgxPaginationModule,
    MatInputModule,
    MyProfileModule,
    TranslateModule
  ],
  exports: [
  ]
})
export class EmployeeModule { }
