import { computed, Injectable, Signal, signal } from '@angular/core';
import { User } from '../models/User';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';

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
  constructor(private cookieService: CookieService) {
    if (cookieService.check('UserJWT')) {
      const helper = new JwtHelperService();
      if (helper.isTokenExpired(cookieService.get('UserJWT'))) {
        cookieService.delete('UserJWT');
      } else {
        this.loginUser(cookieService.get('UserJWT'));
      }
    }
  }

  logout() {
    if (this.currentUser()) {
      this.currentUser.set(null);
    }
  }

  loginUser(JWT: string) {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(JWT);
    this.currentUser.set({
      jwt: JWT,
      email: decodedToken.email,
      id: decodedToken.id,
      phoneNumber: decodedToken.phoneNumber,
      lastname: decodedToken.lastname,
      name: decodedToken.name,
      workerType: decodedToken.workerType,
    });
    this.cookieService.set('UserJWT', JWT);
  }
}
