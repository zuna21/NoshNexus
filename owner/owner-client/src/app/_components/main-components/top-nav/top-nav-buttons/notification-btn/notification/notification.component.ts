import { Component, Input, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { INotification } from 'src/app/_interfaces/INotification';
import { MatRippleModule } from '@angular/material/core';
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { NotificationDialogComponent } from 'src/app/_components/notification-dialog/notification-dialog.component';
import { TimeAgoPipe } from 'src/app/_pipes/time-ago.pipe';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatRippleModule,
    MatDialogModule,
    TimeAgoPipe
  ],
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent {
  @Input('notification') notification: INotification | undefined;

  constructor(
    private dialog: MatDialog
  ) {}


  onNotification() {
    if (!this.notification) return;
    const dialogConfig: MatDialogConfig = {
      data: this.notification
    };
    this.dialog.open(NotificationDialogComponent, dialogConfig);
  }

}
