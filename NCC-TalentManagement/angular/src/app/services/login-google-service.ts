import { BaseApiService } from './base-api.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConsts } from '@shared/AppConsts';

@Injectable({
  providedIn: 'root'
})
export class GoogleLoginService extends BaseApiService {
  constructor(
    http: HttpClient
  ) {
    super(http);
  }

  name() {
    return 'TokenAuth';
  }
  googleAuthenticate(googleToken: string, secretCode: string): Observable<any> {
    return this.http.post(AppConsts.remoteServiceBaseUrl +
      '/api/TokenAuth/GoogleAuthenticate', {googleToken: googleToken, secretCode: secretCode});
  }
}
