import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { GlobalSettingsDTO } from '../models/DTO/GlobalSettingsDTO';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class GlobalSettingsService {
  private _httpClient = inject(HttpClient);
  settings = signal<GlobalSettingsDTO | null>(null);
  constructor() {
    this.SetSettings();
  }

  SetSettings() {
    this._httpClient
      .get(environment.BASE_URL + 'Settings/GetFrontEndSettings')
      .subscribe({
        next: (resData: any) => {
          this.settings.set(resData.data);
          console.log(resData.data);
        },
        error: (err) => {
          console.error(err);
        },
      });
  }
}
