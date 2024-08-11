import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { NavBarComponent } from './core/components/nav-bar/nav-bar.component';
import { FootBarComponent } from './core/components/foot-bar/foot-bar.component';
import { RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';
import { GlobalSettingsService } from './shared/services/global-settings.service';
import { NotificationService } from './shared/services/notification.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: true,
  imports: [RouterModule, NavBarComponent, FootBarComponent],
  providers: [DatePipe],
})
export class AppComponent implements OnDestroy, OnInit {
  title = 'frontend-side-project';
  pom = inject(GlobalSettingsService);
  notificationService = inject(NotificationService);
  private UserOnlineMessageObservableSubscription: Subscription | undefined;
  private UserOfflineMessageObservableSubscription: Subscription | undefined;
  private ReceiveNotificationObservableSubscription: Subscription | undefined;

  ngOnInit(): void {
    this.notificationService.startConnection().subscribe(() => {
      this.SetNotificationObservables();
      this.notificationService.JoinServer();
    });
  }

  ngOnDestroy(): void {
    this.UserOnlineMessageObservableSubscription?.unsubscribe();
    this.UserOfflineMessageObservableSubscription?.unsubscribe();
    this.ReceiveNotificationObservableSubscription?.unsubscribe();
  }

  private SetNotificationObservables() {
    this.UserOnlineMessageObservableSubscription = this.notificationService
      .receiveNotification('UserOnline')
      .subscribe({
        next: (value: string) => {
          console.log(value);
        },
        error: (err) => {
          console.log(err);
        },
      });

    this.UserOfflineMessageObservableSubscription = this.notificationService
      .receiveNotification('UserOffline')
      .subscribe({
        next: (value: string) => {
          console.log(value);
        },
        error: (err) => {
          console.log(err);
        },
      });

    this.ReceiveNotificationObservableSubscription = this.notificationService
      .receiveNotification('ReceiveNotification')
      .subscribe({
        next: (value: string) => {
          console.log(value);
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
