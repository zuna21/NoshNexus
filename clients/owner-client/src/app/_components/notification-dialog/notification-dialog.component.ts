import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { INotification } from 'src/app/_interfaces/INotification';
import { MatButtonModule } from '@angular/material/button';
import { NotificationService } from 'src/app/_services/notification.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-notification-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule
  ],
  templateUrl: './notification-dialog.component.html',
  styleUrls: ['./notification-dialog.component.css'],
})
export class NotificationDialogComponent implements OnInit, OnDestroy {
  notification = this.data;

  markAsReadSub: Subscription | undefined;

  constructor(
    public dialogRef: MatDialogRef<NotificationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: INotification,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
  this.markNotificationAsRead();
  }

  onOk() {
    this.dialogRef.close();
  }

  markNotificationAsRead() {
    if(this.notification.isSeen) return;
    this.markAsReadSub = this.notificationService.markNotificationAsRead(this.notification.id)
      .subscribe({
        next: readNotificationId => {
          if (!readNotificationId) return;
          this.notification.isSeen = true;
        }
      });
  }

  ngOnDestroy(): void {
    this.markAsReadSub?.unsubscribe();
  }

}
