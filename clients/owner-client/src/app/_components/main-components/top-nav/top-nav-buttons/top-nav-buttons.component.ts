import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationBtnComponent } from './notification-btn/notification-btn.component';
import { AccountBtnComponent } from './account-btn/account-btn.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Router, RouterLink } from '@angular/router';
import { MessageBtnComponent } from './message-btn/message-btn.component';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-top-nav-buttons',
  standalone: true,
  imports: [
    CommonModule,
    NotificationBtnComponent,
    AccountBtnComponent,
    MatButtonModule, 
    MatIconModule,
    MatMenuModule,
    RouterLink,
    MessageBtnComponent
  ],
  templateUrl: './top-nav-buttons.component.html',
  styleUrls: ['./top-nav-buttons.component.css']
})
export class TopNavButtonsComponent {

  constructor(
    private accountService: AccountService,
    private router: Router
  ) {}

  onLogOut() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }

}
