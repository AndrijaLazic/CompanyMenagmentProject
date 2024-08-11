import { CommonModule, NgFor } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GlobalSettingsService } from '../../../shared/services/global-settings.service';
import { GlobalUserStateService } from '../../../shared/services/global-user-state.service';
import { UserChatService } from '../../../shared/services/user-chat.service';
import { Observable, Subscription } from 'rxjs';
import { MessageDTS } from '../../../shared/models/DTS/MessageDTS';
import {
  MessageDTR,
  UserCommunication,
} from '../../../shared/models/UserCommunication';
import { User } from '../../../shared/models/User';
import { UserShort } from '../../../shared/models/DTO/GlobalSettingsDTO';

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
  userChatService: UserChatService;
  private UserReceiveMessageObservableSubscription: Subscription | undefined;
  private NewChatCreatedSubscription: Subscription | undefined;
  userChats: UserCommunication[] = [];
  currentChatIndex: number = -1;
  currentChatUser: UserShort | null = null;

  constructor() {
    this.userChatService = inject(UserChatService);
    this.userChatService.startConnection().subscribe(() => {
      this.setSocketObservables();
      this.userChatService.JoinServer();
    });

    this.userChatService.getMyChats().subscribe({
      next: (value: any) => {
        this.userChats = value.data;
        this.userChats.sort();
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  ngOnDestroy(): void {
    this.UserReceiveMessageObservableSubscription?.unsubscribe();
    this.NewChatCreatedSubscription?.unsubscribe();
  }

  messageText = '';

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

  showChat(workerId: number) {
    this.currentChatIndex = this.userChats.findIndex(
      (x) => x.user1 == workerId || x.user2 == workerId,
    );

    if (this.currentChatIndex == -1) {
      this.userChats.push({
        id: -1,
        communicationMessages: [],
        user1: this.globalUserStateService.currentUser()?.id! as number,
        user2: workerId,
        user1Unread: 0,
        user2Unread: 0,
      });
      this.currentChatIndex = 0;
    } else {
      this.userChatService
        .getMessages(this.userChats[this.currentChatIndex].id)
        .subscribe({
          next: (value: any) => {
            const messages: MessageDTR[] = value.data as MessageDTR[];
            if (messages.length > 0) {
              const communication: UserCommunication | undefined =
                this.userChats.find(
                  (x) => (x.id = messages[0].communicationId),
                );
              if (communication) {
                communication.communicationMessages = messages;
              }
            }
          },
          error(err) {
            console.error(err);
          },
        });
    }

    if (
      this.userChats[this.currentChatIndex]!.user1 ==
      this.globalUserStateService.currentUser()?.id
    ) {
      this.currentChatUser = this.globalSettingsService
        .settings()
        ?.users.find(
          (x) => x.id == this.userChats[this.currentChatIndex]?.user2,
        )!;
    } else {
      this.currentChatUser = this.globalSettingsService
        .settings()
        ?.users.find(
          (x) => x.id == this.userChats[this.currentChatIndex]?.user1,
        )!;
    }
  }

  private setSocketObservables() {
    this.UserReceiveMessageObservableSubscription = this.userChatService
      .receiveMessage('ReceiveMessage')
      .subscribe({
        next: (value: any) => {
          const message: MessageDTR = value as MessageDTR;
          console.log(message);
          const communication: UserCommunication | undefined =
            this.userChats.find((x) => (x.id = message.communicationId));
          if (communication) {
            communication.communicationMessages.push(message);
          }
        },
        error: (err) => {
          console.log(err);
        },
      });

    this.NewChatCreatedSubscription = this.userChatService
      .receiveMessage('NewChatCreated')
      .subscribe({
        next: (value: any) => {
          this.userChats[
            this.userChats.findIndex(
              ((x) => x.user1 == value.user1 && x.user2 == value.user2) ||
                ((x) => x.user1 == value.user2 && x.user2 == value.user1),
            )
          ] = value;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  sendMessage() {
    this.userChatService.sendMessage({
      Message: this.messageText,
      receiverId: this.currentChatUser?.id!,
    });
    this.messageText = '';
  }
}
