import { CommonModule, NgFor } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GlobalSettingsService } from '../../../shared/services/global-settings.service';
import { GlobalUserStateService } from '../../../shared/services/global-user-state.service';

@Component({
  selector: 'app-messaging-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './messaging-page.component.html',
  styleUrl: './messaging-page.component.scss',
})
export class MessagingPageComponent {
  globalSettingsService = inject(GlobalSettingsService);
  globalUserStateService = inject(GlobalUserStateService);
  constructor() {}

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

  sendMessage() {
    // this.signalR.SendMessage(this.currentChatId, this.messageText).then(() => {
    //   this.messageText = '';
    // });
  }
}
