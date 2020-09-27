import { Component, OnInit, Inject } from '@angular/core';
import { ExportService } from '@app/services/export-service';
import { ExportFakeService} from '@app/services/export-fake.service'
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/session/app-session.service';

@Component({
  selector: 'app-dialog-export',
  templateUrl: './dialog-export.component.html',
  styleUrls: ['./dialog-export.component.css']
})
export class DialogExportComponent implements OnInit {
  labelPosition: 1 | 2 = 2;
  isHiddenYear = false;
  filePathofExport: string;
  fileNameExport: string;
  isSale: boolean;
  isUser: boolean;
  isEmployee = false;
  id: number;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private exportService: ExportService,
    private _dialogRef: MatDialogRef<DialogExportComponent>,
    private _permissionChecker: PermissionCheckerService,
    private exportFakeService: ExportFakeService,
    private session: AppSessionService,
  ) { }
  ngOnInit(): void {
    this.isSale = this._permissionChecker.isGranted('Pages.EditAsSales.Employee');
     this.isUser = this.data[1];
  }

  exportCV() {
    if (this.isSale && !this.isUser) {
      this.data[0].typeOffile = this.labelPosition;
      this.data[0].isHiddenYear = this.isHiddenYear;
      this.exportFakeService.ExportCVFake(this.data[0]).subscribe(res =>{
      this.filePathofExport = res.result;
      this.fileNameExport = 'my_profile';
      this._dialogRef.close(res);
      this.downloadURI(this.filePathofExport, this.fileNameExport);
      });
    } else {
      this.exportService.exportCV(this.data[0].userId, this.labelPosition, this.isHiddenYear).subscribe(res => {
        this.filePathofExport = res.result;
        this.fileNameExport = 'my_profile';
        this._dialogRef.close(res);
        this.downloadURI(this.filePathofExport, this.fileNameExport);
      });
    }
  }
  downloadURI(uri, fileName) {
    const link = document.createElement('a');
    link.download = fileName;
    link.href = uri;
    link.target = 'blank';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}
