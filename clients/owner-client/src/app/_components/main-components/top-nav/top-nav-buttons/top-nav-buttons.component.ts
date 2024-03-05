import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationBtnComponent } from './notification-btn/notification-btn.component';
import { AccountBtnComponent } from './account-btn/account-btn.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { LanguageBtnComponent } from './language-btn/language-btn.component';
import {MatBottomSheet, MatBottomSheetModule} from '@angular/material/bottom-sheet'; 
import { LanguageBottomSheetComponent } from './language-bottom-sheet/language-bottom-sheet.component';

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
    LanguageBtnComponent,
    MatBottomSheetModule
  ],
  templateUrl: './top-nav-buttons.component.html',
  styleUrls: ['./top-nav-buttons.component.css']
})
export class TopNavButtonsComponent implements OnInit {
  role: string = '';

  constructor(
    private _accountService: AccountService,
    private _router: Router,
    private _bottomSheet: MatBottomSheet
  ) {}

  ngOnInit(): void {
    this.role = this._accountService.getRole() ?? '';
  }

  onLogOut() {
    this._accountService.logout();
    this._router.navigateByUrl('/login');
  }

  selectLanguage() {
    this._bottomSheet.open(LanguageBottomSheetComponent);
  }

}
