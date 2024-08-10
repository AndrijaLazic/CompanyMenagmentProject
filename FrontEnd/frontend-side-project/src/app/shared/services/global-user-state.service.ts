import { computed, Injectable, Signal, signal } from '@angular/core';
import { User } from '../models/User';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { TokensDTO } from '../models/DTO/TokensDTO';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class GlobalUserStateService {
  currentUser = signal<User | null>(null);
  notificationNumberMessages = signal<number>(0);
  notificationNumberBasic = signal<number>(0);
  notificationNumberTotal: Signal<number> = computed(
    () => this.notificationNumberMessages() + this.notificationNumberBasic(),
  );
  constructor(
    private cookieService: CookieService,
    private authService: AuthService,
    private router: Router,
  ) {
    this.checkJWT();
  }

  checkJWT() {
    const jwt = sessionStorage.getItem('UserJWT');
    const helper = new JwtHelperService();

    if (jwt) {
      if (!helper.isTokenExpired(jwt)) {
        this.loginUser({
          jwt: jwt,
          resetToken: this.cookieService.get('ResetToken'),
        });
        return;
      }
    }

    if (this.cookieService.check('ResetToken')) {
      if (helper.isTokenExpired(this.cookieService.get('ResetToken'))) {
        this.logout();
      } else {
        const tokens: TokensDTO = {
          jwt: '',
          resetToken: this.cookieService.get('ResetToken'),
        };

        this.authService.resetJWT(tokens).subscribe({
          next: (resData: any) => {
            this.loginUser({
              jwt: resData.data,
              resetToken: this.cookieService.get('ResetToken'),
            });
          },
          error: (err) => {
            console.log(err);
            this.router.navigate(['/']);
          },
        });
      }
    }
  }

  logout() {
    if (this.currentUser()) {
      this.currentUser.set(null);
    }

    this.cookieService.delete('ResetToken');

    sessionStorage.removeItem('UserJWT');
  }

  loginUser(tokens: TokensDTO) {
    this.logout();
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(tokens.jwt);
    this.currentUser.set({
      jwt: tokens.jwt,
      email: decodedToken.email,
      id: +decodedToken.id,
      phoneNumber: decodedToken.phoneNumber,
      lastname: decodedToken.lastname,
      name: decodedToken.name,
      workerType: decodedToken.workerType,
    });
    sessionStorage.setItem('UserJWT', tokens.jwt);
    this.cookieService.set('ResetToken', tokens.resetToken, 10, '/');
  }
}
