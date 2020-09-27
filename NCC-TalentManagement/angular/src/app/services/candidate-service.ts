import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CandidateService extends BaseApiService {

  constructor(http: HttpClient) {
    super(http);
  }
  name() {
    return 'Candidate';
  }
  uploadAttachments(files: any): Observable<any> {
    return this.http.post(this.rootUrl + '/UploadAttachments', files);
  }
  cancelUploadFile(data: any): Observable<any> {
    return this.http.post(this.rootUrl + '/cancelUploadFile', data);
  }

  insertOrUpdateCandidate(data: any): Observable<any> {
    return this.http.post(this.rootUrl + '/InsertOrUpdateCandidate', data);
  }
  GetAllCandidatePaging(Search: string, Skill: any, BranchId: any, Status: any, MonthReceived: any, YearReceived: any,
     MaxResultCount: number, SkipCount: any): Observable<any> {
    return this.http.get(this.rootUrl + '/GetAllCandidatePaging?Search=' + Search +
                                        '&Skill=' + Skill + '&BranchId=' + BranchId + '&Status=' + Status + '&MonthReceived='
                                        + MonthReceived + '&YearReceived=' + YearReceived + '&MaxResultCount=' + MaxResultCount + '&SkipCount=' + SkipCount);
  }
  getCandidateInfoById(candidateId: number): Observable<any> {
    return this.http.get(this.getUrl(`GetCandidateInfoById?candidateId=${candidateId}`));
  }

  deleteCandidate(candidateId: number): Observable<any> {
    return this.http.delete(this.rootUrl + '/deleteCandidate?candidateId=' + candidateId);
  }

}