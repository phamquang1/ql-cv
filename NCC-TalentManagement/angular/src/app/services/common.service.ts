import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonService extends BaseApiService {

  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'Common';
  }

  GetAllPositionType(): Observable<any> {
    return this.http.get(this.getUrl(`GetAllPositionType`));
  }
  GetAllBranch(): Observable<any> {
    return this.http.get(this.getUrl(`GetAllBranch`));
  }
  getComboboxGroupSkill(): Observable<any> {
    return this.http.get(this.getUrl(`GetComboboxGroupSkill`));
  }
  getSkillByGroupSkillId(id): Observable<any> {
      return this.http.get(this.getUrl(`GetCBBSkillByGroupSkillId?id=${id}`));
  }
  getCBBInterviewer(search: string, SkipCount: number, MaxResultCount: number ): Observable<any> {
    return this.http.get(this.getUrl(`GetCBBInterviewer?search=${search}&SkipCount=${SkipCount}&MaxResultCount=${MaxResultCount}`));
  }

  getCBBSkillForCandidate(search: string, SkipCount: number, MaxResultCount: number ): Observable<any> {
    return this.http.get(this.getUrl(`GetCBBSkillForCandidate?search=${search}&SkipCount=${SkipCount}&MaxResultCount=${MaxResultCount}`));
  }

  getCBBPresenter(search: string, SkipCount: number, MaxResultCount: number ): Observable<any> {
    return this.http.get(this.getUrl(`GetCBBPresenter?search=${search}&SkipCount=${SkipCount}&MaxResultCount=${MaxResultCount}`));
  }

  getCBBOldCVId(search: string, SkipCount: number, MaxResultCount: number ): Observable<any> {
    return this.http.get(this.getUrl(`GetCBBOldCVId?search=${search}&SkipCount=${SkipCount}&MaxResultCount=${MaxResultCount}`));
  }
  GetCBBStatusCandidate(): Observable<any> {
    return this.http.get(this.rootUrl + '/GetCBBStatusCandidate');
  }

}