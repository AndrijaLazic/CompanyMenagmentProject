import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export class UserChatService {
  private hubConnection: signalR.HubConnection;
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
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  JoinChatOfUser(userId: number) {
    this.hubConnection.invoke('JoinChatOfUser', userId);
  }

  receiveMessage(message: string): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on(message, (message: string) => {
        observer.next(message);
      });
    });
  }

  sendMessage(message: string): void {
    this.hubConnection.invoke('JoinServer', message);
  }
}
