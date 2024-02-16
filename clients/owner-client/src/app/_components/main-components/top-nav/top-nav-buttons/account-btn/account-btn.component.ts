import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-account-btn',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatMenuModule,
    MatIconModule,
    RouterLink,
    TranslateModule
  ],
  templateUrl: './account-btn.component.html',
  styleUrls: ['./account-btn.component.css'],
})
export class AccountBtnComponent implements OnInit {
  @Output('logout') logout = new EventEmitter<boolean>();
  isProfileImageLoading: boolean = true;
  role: string = ''

  constructor(
    public accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.role = this.accountService.getRole() ?? '';
  }

  onLogOut() {
    this.logout.emit(true);
  }
}
