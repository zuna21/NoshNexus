import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationComponent } from 'src/app/_components/main-components/top-nav/top-nav-buttons/notification-btn/notification/notification.component';
import { NotificationService } from 'src/app/_services/notification.service';
import { INotification } from 'src/app/_interfaces/INotification';
import { Subscription } from 'rxjs';
import { MatDividerModule } from '@angular/material/divider';
import {ScrollingModule} from '@angular/cdk/scrolling'; 
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { NotificationHubService } from 'src/app/_services/notification-hub.service';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [
    CommonModule, 
    NotificationComponent,
    MatDividerModule,
    ScrollingModule,
    SearchBarComponent,
    MatCheckboxModule
  ],
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css'],
})
export class NotificationsComponent implements OnInit, OnDestroy {
  notifications: INotification[] = [];

  notificationSub: Subscription | undefined;
  liveNotificationSub: Subscription | undefined;

  constructor(
    private notificationService: NotificationService,
    private notificationHubService: NotificationHubService
  ) {}

  ngOnInit(): void {
    this.getNotifications();
    this.getLiveNotification();
  }

  getNotifications() {
    this.notificationSub = this.notificationService
      .getAllNotifications()
      .subscribe({
        next: (notifications) => (this.notifications = notifications),
      });
  }

  getLiveNotification() {
    this.liveNotificationSub = this.notificationHubService.receiveNotificationForMenu().subscribe({
      next: notification => this.notifications = [notification, ...this.notifications]
    });
  }


  ngOnDestroy(): void {
    this.notificationSub?.unsubscribe();
    this.liveNotificationSub?.unsubscribe();
  }
}
