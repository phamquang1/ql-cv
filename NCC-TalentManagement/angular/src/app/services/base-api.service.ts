import { InjectionToken } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConsts } from '../../shared/AppConsts';
export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');


export abstract class BaseApiService {
  protected http: HttpClient;
  constructor(http: HttpClient) {
      this.http = http;
  }

  protected get rootUrl() {
    return AppConsts.remoteServiceBaseUrl + '/api/services/app/' + this.name();
}

abstract name();

protected getUrl(url: string) {
  return this.rootUrl + '/' + url;
}

}
