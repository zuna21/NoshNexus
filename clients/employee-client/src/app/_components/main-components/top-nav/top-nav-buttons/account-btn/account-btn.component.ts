import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-account-btn',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatMenuModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './account-btn.component.html',
  styleUrls: ['./account-btn.component.css'],
})
export class AccountBtnComponent {
  @Output('logout') logout = new EventEmitter<boolean>();
  isProfileImageLoading: boolean = true;

  onLogOut() {
    this.logout.emit(true);
  }
}
