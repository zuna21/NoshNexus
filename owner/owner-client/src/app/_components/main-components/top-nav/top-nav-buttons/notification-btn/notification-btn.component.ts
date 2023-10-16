import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatBadgeModule} from '@angular/material/badge'; 
import {MatChipsModule} from '@angular/material/chips'; 
import { NotificationComponent } from './notification/notification.component';
import { MatDividerModule } from '@angular/material/divider';

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
export class NotificationBtnComponent {
  openNotifications: boolean = true;

}
