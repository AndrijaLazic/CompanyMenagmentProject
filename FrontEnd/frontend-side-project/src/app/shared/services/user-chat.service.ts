import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { GlobalUserStateService } from './global-user-state.service';
import { MessageDTS } from '../models/DTS/MessageDTS';
import { HttpClient } from '@angular/common/http';

export class UserChatService {
  private hubConnection: signalR.HubConnection;
  private globalUser = inject(GlobalUserStateService);
  private _httpClient = inject(HttpClient);
  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.BASE_URL + 'user-chat')
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

  receiveMessage(message: string): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on(message, (message: string) => {
        observer.next(message);
      });
    });
  }

  sendMessage(message: MessageDTS): void {
    this.hubConnection.invoke('SendMessage', message);
  }

  getMyChats() {
    return this._httpClient.get(environment.BASE_URL + 'Chat/GetMyChats');
  }

  getMessages(chatId: number) {
    return this._httpClient.get(
      environment.BASE_URL + 'Chat/GetMessages/' + chatId,
    );
  }
}
