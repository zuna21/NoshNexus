import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { INotification } from 'src/app/_interfaces/INotification';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule
  ],
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent {
  @Input('notification') notification: INotification | undefined;
}
