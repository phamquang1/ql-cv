import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { NccCvComponent } from './ncc-cv.component';


const routes: Routes = [
  {
    path: 'my-profile',
    component: NccCvComponent,
    canActivate: [AppRouteGuard],
    children: [{
      path: '',
      children: [
        {
          path: '',
          loadChildren: () => import('./my-profile/my-profile.module').then(m => m.MyProfileModule),
          data: {
            permission: 'Page.MyProfile',
            preload: true
          },
          canActivate: [AppRouteGuard],
        }
      ]
    }]
  },
  {
    path: 'candidate',
    component: NccCvComponent,
    canActivate: [AppRouteGuard],
    children: [{
      path: '',
      children: [
        {
          path: '',
          loadChildren: () => import('./candidate/candidate.module').then(m => m.CandidateModule),
          data: {
            permission: 'Pages.CVCandidate',
            preload: true
          },
          canActivate: [AppRouteGuard],
        }
      ]
    }]
  },
  {
    path: 'employee',
    component: NccCvComponent,
    canActivate: [AppRouteGuard],
    children: [{
      path: '',
      children: [
        {
          path: '',
          loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule),
          data: {
            permission: 'Pages.CVEmployee',
            preload: true
          },
          canActivate: [AppRouteGuard],
        }
      ]
    }]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NccCvRoutingModule { }
