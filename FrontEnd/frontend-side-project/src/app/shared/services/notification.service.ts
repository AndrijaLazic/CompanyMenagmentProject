import { inject, Injectable } from '@angular/core';
import { GlobalUserStateService } from './global-user-state.service';
import { GlobalSettingsService } from './global-settings.service';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private hubConnection: signalR.HubConnection;
  private globalUser = inject(GlobalUserStateService);
  private _httpClient = inject(HttpClient);
  private _globalSettings = inject(GlobalSettingsService);
  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.BASE_URL + 'notifications')
      .build();
  }

  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          this.JoinServer();
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  JoinServer() {
    this.hubConnection.invoke('JoinServer', this.globalUser.currentUser()?.jwt);
  }

  receiveNotification(notification: string): Observable<any> {
    return new Observable<any>((observer) => {
      this.hubConnection.on(notification, (message: string) => {
        observer.next(message);
      });
    });
  }
}
