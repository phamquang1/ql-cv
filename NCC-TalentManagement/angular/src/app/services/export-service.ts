import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExportService extends BaseApiService{
  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'ExportDocService';
  }
  exportCV(userId: number, typeOffile: number, isHiddenYear: boolean): Observable<any> {
    return this.http.get(this.getUrl(`ExportCV?userId=${userId}&typeOffile=${typeOffile}&isHiddenYear=${isHiddenYear}`));
  }
}
