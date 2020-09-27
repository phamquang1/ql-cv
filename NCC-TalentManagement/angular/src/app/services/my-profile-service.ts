import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MyProfileService extends BaseApiService {

  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'MyProfile';
  }
// Lấy thông tin nhân viên
 getUserGeneralInfo(userId): Observable<any> {
    return this.http.get(this.getUrl(`GetUserGeneralInfo?userId=${userId}`));
  }
// Lấy thông tin công nghệ,
 getTechnicalExpertise(userId): Observable<any> {
    return this.http.get(this.getUrl(`GetTechnicalExpertise?userId=${userId}`));
  }
  SaveUserGeneralInfo(data: any): Observable<any> {
    return this.http.post(this.rootUrl + '/SaveUserGeneralInfo', data);
  }
  SaveEducation(data: object): Observable<any> {
    return this.http.post(this.rootUrl + '/SaveEducation', data);
  }
  DeleteEducation(id: number): Observable<any> {
    return this.http.delete(this.rootUrl + '/DeleteEducation?id=' + id);
  }

  UpdateOrderEducation(data: any): Observable<any> {
    return this.http.put(this.rootUrl + '/UpdateOrderEducation', data);
  }
// Lấy thông tin kinh nghiệm công việc
  getUserWorkingExperience(userId): Observable<any> {
  return this.http.get(this.getUrl(`GetUserWorkingExperience?userId=${userId}`));
  }
  UpdateOrderWorkingExperience(data: any): Observable<any> {
    return this.http.put(this.rootUrl + '/UpdateOrderWorkingExperience', data);
  }
  UpdateWorkingExperience(data: Object): Observable<any> {
    return this.http.put(this.rootUrl + '/UpdateWorkingExperience', data);
  }
  DeleteWorkingExperience(id: number): Observable<any> {
    return this.http.delete(this.rootUrl + '/DeleteWorkingExperience?id=' + id);
  }
  // Lấy thông tin giáo dục
  getEducationInfo(userId): Observable<any> {
    return this.http.get(this.getUrl(`GetEducationInfo?userId=${userId}`));
  }
  // Lấy thông tin thuộc tính cá nhân
  getPersonalAttribute(userId): Observable<any> {
    return this.http.get(this.getUrl(`GetPersonalAttribute?userId=${userId}`));
  }
  // Update technical expertise
   updateTechnicalExpertise(data: any): Observable<any> {
     return this.http.put(this.rootUrl + '/UpdateTechnicalExpertise', data);
   }
  // Update Person atribute
  updatePersonalAttribute(data: any): Observable<any> {
    return this.http.put(this.rootUrl + '/UpdatePersonalAttribute', data);
  }
  
}