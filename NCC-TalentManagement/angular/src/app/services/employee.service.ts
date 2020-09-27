import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends BaseApiService{
  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'Employee';
  }
  GetAllEmployeePaging(data: Object): Observable<any> {
    return this.http.post(this.rootUrl + '/GetAllEmployeePaging' , data);
  }
  GetWorkingExperiencePaging(tech: string): Observable<any> {
    return this.http.get(this.rootUrl + '/GetWorkingExperiencePaging?technologies=' + tech);
  }
}
