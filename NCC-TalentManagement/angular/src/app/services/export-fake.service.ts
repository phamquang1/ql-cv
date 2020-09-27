import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExportFakeService extends BaseApiService{

  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'ExportFakeForSaleService';
  }
  ExportCVFake(data: object): Observable<any> {
    return this.http.post(this.rootUrl + '/ExportCVFake', data);
  }
}
