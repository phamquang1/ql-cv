import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from './employee.component';
import { DetailEmployeeComponent } from './detail-employee/detail-employee.component';


const routes: Routes = [
  {
    path: '',
    component : EmployeeComponent
  },
  {
    path: 'detail-employee/:id',
    component: DetailEmployeeComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
