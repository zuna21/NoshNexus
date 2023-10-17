import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { INotification } from 'src/app/_interfaces/INotification';
import { MatButtonModule } from '@angular/material/button';

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
export class NotificationDialogComponent {
  notification = this.data;

  constructor(
    public dialogRef: MatDialogRef<NotificationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: INotification
  ) {}

  onOk() {
    this.dialogRef.close();
  }
}
