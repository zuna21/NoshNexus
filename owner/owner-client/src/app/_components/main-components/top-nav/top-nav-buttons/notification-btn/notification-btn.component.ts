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
  ],
  templateUrl: './notification-btn.component.html',
  styleUrls: ['./notification-btn.component.css'],
})
export class NotificationBtnComponent implements OnInit, OnDestroy {
  openNotifications: boolean = false;
  notificationMenu: INotificationsForMenu | undefined;

  notificationSub: Subscription | undefined;

  constructor(
    private notificationService: NotificationService,
    private eRef: ElementRef
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

  onAllAsRead() {
    if (!this.notificationMenu) return;
    this.notificationMenu.notSeenNumber = 0;
    this.notificationMenu.notifications.map((x) => (x.isSeen = true));
  }

  @HostListener('document:click', ['$event'])
  clickout(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.openNotifications = false;
    }
  }

  ngOnDestroy(): void {
    this.notificationSub?.unsubscribe();
  }
}
