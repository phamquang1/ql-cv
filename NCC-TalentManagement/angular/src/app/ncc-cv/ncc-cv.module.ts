import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NccCvRoutingModule } from './ncc-cv-routing.module';
import { NccCvComponent } from './ncc-cv.component';


@NgModule({
  declarations: [NccCvComponent],
  imports: [
    CommonModule,
    NccCvRoutingModule
  ]
})
export class NccCvModule { }
