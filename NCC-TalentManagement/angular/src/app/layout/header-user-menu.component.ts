import { Component, ChangeDetectionStrategy } from '@angular/core';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { NavigationExtras, ActivatedRoute, Router } from '@angular/router';
import { Route } from '@angular/compiler/src/core';
import { SocialAuthService } from 'angularx-social-login';

@Component({
  selector: 'header-user-menu',
  templateUrl: './header-user-menu.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderUserMenuComponent {
  constructor(
    private _authService: AppAuthService,
    private authServiceGoogle: SocialAuthService,
    private _router : Router
    ) {}

  logout(): void {
    this._authService.logout();
    this.authServiceGoogle.signOut()
    // this._router.navigate(['/account/login'], { clearHistory: true } as NavigationExtras)
  }
}
