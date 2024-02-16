import {
  Component,
  ElementRef,
  HostListener,
  OnDestroy,
  OnInit,
} from '@angular/core';
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
import { RouterLink } from '@angular/router';
import { MatRippleModule } from '@angular/material/core';
import { TranslateModule } from '@ngx-translate/core';

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
    MatDividerModule,
    RouterLink,
    MatRippleModule,
    TranslateModule
  ],
  templateUrl: './notification-btn.component.html',
  styleUrls: ['./notification-btn.component.css'],
})
export class NotificationBtnComponent implements OnInit, OnDestroy {
  openNotifications: boolean = false;
  notificationMenu: INotificationsForMenu | undefined;

  notificationSub: Subscription | undefined;
  allAsReadSub: Subscription | undefined;
  notificationHubSub: Subscription | undefined;

  constructor(
    private notificationService: NotificationService,
    private eRef: ElementRef,
  ) {}

  ngOnInit(): void {
    this.getNotifications();
  }

  getNotifications() {
    this.notificationSub = this.notificationService
      .getOwnerNotificationsForMenu()
      .subscribe({
        next: (notifications) => (this.notificationMenu = notifications),
      });
  }

  notificationOpen(notificationId: number) {
    if (!this.notificationMenu) return;
    this.notificationMenu.notSeenNumber =
      this.notificationMenu.notSeenNumber - 1;
    this.notificationMenu.notifications =
      this.notificationMenu.notifications.filter(
        (x) => x.id !== notificationId
      );
  }


  onAllAsRead() {
    if (!this.notificationMenu || this.notificationMenu.notifications.length <= 0) return;
    this.allAsReadSub = this.notificationService.markAllNotificationsAsRead().subscribe({
      next: areAllRead => {
        if (!areAllRead || !this.notificationMenu) return;
        this.notificationMenu.notSeenNumber = 0;
        this.notificationMenu.notifications = [];
      }
    });
  }

  @HostListener('document:click', ['$event'])
  clickout(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.openNotifications = false;
    }
  }


  ngOnDestroy(): void {
    this.notificationSub?.unsubscribe();
    this.allAsReadSub?.unsubscribe();
    this.notificationHubSub?.unsubscribe();
  }
}
