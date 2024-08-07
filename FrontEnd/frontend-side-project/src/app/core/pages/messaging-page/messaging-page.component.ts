import { CommonModule, NgFor } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GlobalSettingsService } from '../../../shared/services/global-settings.service';
import { GlobalUserStateService } from '../../../shared/services/global-user-state.service';
import { UserChatService } from '../../../shared/services/user-chat.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-messaging-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './messaging-page.component.html',
  styleUrl: './messaging-page.component.scss',
  providers: [UserChatService],
})
export class MessagingPageComponent implements OnInit, OnDestroy {
  globalSettingsService = inject(GlobalSettingsService);
  globalUserStateService = inject(GlobalUserStateService);
  userChatService = inject(UserChatService);
  private UserOnlineMessageObservableSubscription: Subscription | undefined;
  private UserOfflineMessageObservableSubscription: Subscription | undefined;

  constructor() {
    this.userChatService.startConnection().subscribe(() => {
      this.setSocketObservables();
    });
  }

  ngOnDestroy(): void {
    this.UserOnlineMessageObservableSubscription?.unsubscribe();
  }

  currentChatId = -1;
  currentChatUser: Worker | null = null;

  messageText = '';

  messages: { message: string; senderId: number }[] = [
    { senderId: 1, message: 'Pozdrav kolega!!!' },
    { senderId: 1, message: 'Zasto danas niste bili na poslu?' },
    { senderId: 2, message: 'Imao sam zdravstvenih problema' },
  ];

  ngOnInit(): void {
    // this.signalR.startConnection(localStorage.getItem('JWT')!);
    // this.chatService.getWorkers().subscribe((response: any) => {
    //   this.signalR.workers.set(response.data);
    //   const userData = this.signalR
    //     .workers()
    //     .find((x) => x.id == this.globalState.getWorkerData().id);
    //   if (userData) {
    //     this.signalR
    //       .workers()
    //       .splice(this.signalR.workers().indexOf(userData), 1);
    //   }
    // });
  }

  showChat(id: number) {
    // this.chatService.getWorkerChat(id).subscribe((response: any) => {
    //   const newChat = new CurrentChat();
    //   const userWithID = this.signalR.workers().find((x) => x.id === id);
    //   if (userWithID) {
    //     newChat.user = userWithID;
    //   }
    //   newChat.messages = response.data;
    //   this.signalR.currentChat.set(newChat);
    //   this.currentChatId = id;
    //   this.signalR
    //     .JoinChatWithUser(+this.globalState.getWorkerData().id!, id)
    //     .then(() => {
    //       const currenChatMessagesElement = document.getElementById(
    //         'current-chat-messages',
    //       );
    //       if (currenChatMessagesElement) {
    //         currenChatMessagesElement.scrollTop =
    //           currenChatMessagesElement.scrollHeight;
    //       }
    //     });
    // });
  }

  private setSocketObservables() {
    this.UserOnlineMessageObservableSubscription = this.userChatService
      .receiveMessage('UserOnline')
      .subscribe({
        next(value: string) {
          console.log(value);
        },
        error(err) {
          console.log(err);
        },
      });

    this.UserOfflineMessageObservableSubscription = this.userChatService
      .receiveMessage('UserOffline')
      .subscribe({
        next(value: string) {
          console.log(value);
        },
        error(err) {
          console.log(err);
        },
      });
  }

  sendMessage() {
    this.userChatService.sendMessage(
      this.globalUserStateService.currentUser()?.jwt!,
    );
  }
}
