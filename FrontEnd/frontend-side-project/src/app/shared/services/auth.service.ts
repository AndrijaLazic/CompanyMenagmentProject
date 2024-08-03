import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginDTS } from '../models/DTS/LoginDTS';
import { environment } from '../../../environments/environment';
import { GlobalUserStateService } from './global-user-state.service';
import { TokensDTO } from '../models/DTO/TokensDTO';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _httpClient = inject(HttpClient);
  login(loginData: LoginDTS) {
    return this._httpClient.post(
      environment.BASE_URL + 'Auth/Login',
      loginData,
    );
  }

  resetJWT(resetToken: TokensDTO) {
    return this._httpClient.post(
      environment.BASE_URL + 'Auth/ResetJWT',
      resetToken,
    );
  }
}
