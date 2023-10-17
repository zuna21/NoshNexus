import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationBtnComponent } from './notification-btn/notification-btn.component';
import { AccountBtnComponent } from './account-btn/account-btn.component';

@Component({
  selector: 'app-top-nav-buttons',
  standalone: true,
  imports: [
    CommonModule,
    NotificationBtnComponent,
    AccountBtnComponent
  ],
  templateUrl: './top-nav-buttons.component.html',
  styleUrls: ['./top-nav-buttons.component.css']
})
export class TopNavButtonsComponent {

}
