import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';
import { NotificationComponent } from './notification/notification.component';
import { MatDividerModule } from '@angular/material/divider';
import { INotificationsForMenu } from 'src/app/_interfaces/INotification';
import { Subscription } from 'rxjs';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-notification-btn',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatBadgeModule,
    MatChipsModule,
    NotificationComponent,
    MatDividerModule
  ],
  templateUrl: './notification-btn.component.html',
  styleUrls: ['./notification-btn.component.css']
})
export class NotificationBtnComponent implements OnInit, OnDestroy {
  openNotifications: boolean = true;
  notificationMenu: INotificationsForMenu | undefined;

  notificationSub: Subscription | undefined;

  constructor(
    private notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    this.getNotifications();
  }

  getNotifications() {
    this.notificationSub = this.notificationService.getOwnerNotifications().subscribe({
      next: notifications => this.notificationMenu = notifications
    });
  }

  onAllAsRead() {
    if (!this.notificationMenu) return;
    this.notificationMenu.notSeenNumber = 0;
    this.notificationMenu.notifications.map(x => x.isSeen = true);
  }

  ngOnDestroy(): void {
    this.notificationSub?.unsubscribe();
  }
}
