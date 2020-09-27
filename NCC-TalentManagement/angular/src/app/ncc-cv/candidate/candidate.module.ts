import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CandidateRoutingModule } from './candidate-routing.module';
import { CandidateComponent } from './candidate.component';
import { SharedModule } from '@shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatInputModule } from '@angular/material/input';
import { CreateCandidateComponent } from './create-candidate/create-candidate.component';
import {TranslateModule} from '@ngx-translate/core';

@NgModule({
  declarations: [CandidateComponent, CreateCandidateComponent],
  imports: [
    CommonModule,
    CandidateRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    FormsModule,
    MatInputModule,
    TranslateModule
  ]
})
export class CandidateModule { }
