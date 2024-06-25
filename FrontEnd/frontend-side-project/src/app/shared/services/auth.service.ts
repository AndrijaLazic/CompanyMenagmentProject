import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginDTS } from '../models/DTS/LoginDTS';
import { environment } from '../../../environments/environment';
import { GlobalUserStateService } from './global-user-state.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _httpClient = inject(HttpClient);
  private _globalUserState = inject(GlobalUserStateService);
  login(loginData: LoginDTS) {
    this._httpClient
      .post(environment.BASE_URL + 'Auth/Login', loginData)
      .subscribe({
        next: (resData: any) => {
          this._globalUserState.loginUser(resData.data);
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
