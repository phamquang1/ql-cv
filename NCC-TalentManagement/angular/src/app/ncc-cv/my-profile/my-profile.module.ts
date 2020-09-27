import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// import {MatDialogModule} from '@angular/material/dialog';
// import {MatAutocompleteModule} from '@angular/material/autocomplete';
// import {MatBadgeModule} from '@angular/material/badge'
// import {MatIconModule} from '@angular/material/icon'
import {MatFormFieldModule} from '@angular/material/form-field';
// import {MatSelectModule} from '@angular/material/select';


import {MatAutocompleteModule} from '@angular/material/autocomplete'
import {MatBadgeModule} from '@angular/material/badge'
import {MatBottomSheetModule} from '@angular/material/bottom-sheet'
import {MatButtonModule} from '@angular/material/button'
import {MatButtonToggleModule} from '@angular/material/button-toggle'
import {MatCardModule} from '@angular/material/card'
import {MatCheckboxModule} from '@angular/material/checkbox'
import {MatChipsModule} from '@angular/material/chips'
import {MatDatepickerModule} from '@angular/material/datepicker'
import {MatDialogModule} from '@angular/material/dialog'
import {MatDividerModule} from '@angular/material/divider'
import {MatExpansionModule} from '@angular/material/expansion'
import {MatGridListModule} from '@angular/material/grid-list'
import {MatIconModule} from '@angular/material/icon'
import {MatInputModule} from '@angular/material/input'
import {MatListModule} from '@angular/material/list'
import {MatMenuModule} from '@angular/material/menu'
import {MatNativeDateModule} from '@angular/material/core'
import {MatPaginatorModule} from '@angular/material/paginator'
import {MatProgressBarModule} from '@angular/material/progress-bar'
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'
import {MatRadioModule} from '@angular/material/radio'
import {MatSelectModule} from '@angular/material/select'
import {MatSidenavModule} from '@angular/material/sidenav'
import {MatSliderModule} from '@angular/material/slider'
import {MatSlideToggleModule} from '@angular/material/slide-toggle'
import {MatSnackBarModule} from '@angular/material/snack-bar'
import {MatSortModule} from '@angular/material/sort'
import {MatStepperModule} from '@angular/material/stepper'
import {MatTableModule} from '@angular/material/table'
import {MatTabsModule} from '@angular/material/tabs'
import {MatToolbarModule} from '@angular/material/toolbar'
import {MatTooltipModule} from '@angular/material/tooltip'
import {MatTreeModule} from '@angular/material/tree'
import {DragDropModule} from '@angular/cdk/drag-drop';
import { MyProfileRoutingModule } from './my-profile-routing.module';
import { MyProfileComponent } from './my-profile.component';
import { SharedModule } from '@shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { DialogExportComponent } from './dialog-export/dialog-export.component';
import {TranslateModule} from '@ngx-translate/core';
@NgModule({
  declarations: [
    MyProfileComponent,
    DialogExportComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MyProfileRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    NgxPaginationModule,

    //
    MatProgressBarModule,
    MatDialogModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,

    //
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule ,
    MatTableModule ,
    MatTabsModule,
    MatToolbarModule ,
    MatTooltipModule,
    MatTreeModule,
    DragDropModule,
    TranslateModule
  ],
  exports: [
    MyProfileComponent
  ],
  entryComponents: [DialogExportComponent
  ],
})
export class MyProfileModule { }
