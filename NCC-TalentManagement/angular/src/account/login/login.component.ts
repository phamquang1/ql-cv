import { Component, Injector, OnInit } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { SocialAuthService, FacebookLoginProvider, AmazonLoginProvider } from 'angularx-social-login';
import { GoogleLoginProvider, SocialUser  } from 'angularx-social-login';
import { GoogleLoginService } from 'app/services/login-google-service';

@Component({
  templateUrl: './login.component.html',
  animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase implements OnInit {
  submitting = false;
  user: SocialUser;
  loggedIn: boolean;
  nccCode: string;
  constructor(
    injector: Injector,
    public authService: AppAuthService,
    private _sessionService: AbpSessionService,
    private authServiceGoogle: SocialAuthService,
    private googleLoginService: GoogleLoginService
  ) {
    super(injector);
  }
  get multiTenancySideIsTeanant(): boolean {
    return this._sessionService.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    if (!this._sessionService.tenantId) {
      return false;
    }

    return true;
  }
  ngOnInit() {
    this.authServiceGoogle.authState.subscribe(user => {
      this.user = user;
      this.loggedIn = (this.user != null);
      if (this.loggedIn) {
         this.authService.authenticateGoogle(this.user.idToken, this.nccCode);
      }
    });
  }

  login(): void {
    this.submitting = true;
    this.authService.authenticate(() => (this.submitting = false));
  }

  signInWithGoogle(): void {
    this.authServiceGoogle.signIn(GoogleLoginProvider.PROVIDER_ID);
    
  }
}
