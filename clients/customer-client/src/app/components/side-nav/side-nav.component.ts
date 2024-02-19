import { Component, OnDestroy } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { MatRippleModule } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateModule } from '@ngx-translate/core';
import { TitleCasePipe } from '@angular/common';
import {MatBottomSheet, MatBottomSheetModule} from '@angular/material/bottom-sheet'; 
import { LanguageBottomSheetComponent } from '../language-bottom-sheet/language-bottom-sheet.component';
import { SideNavService } from './side-nav.service';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    MatIconModule,
    MatRippleModule,
    TranslateModule,
    TitleCasePipe,
    MatBottomSheetModule
  ],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.css'
})
export class SideNavComponent implements OnDestroy {

  orderHistoryLoginSub?: Subscription;
  accountSub?: Subscription;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private _bottomSheet: MatBottomSheet,
    private sideNavService: SideNavService
  ) {}

  
  onViewOrderHistory() {
    if (!this.accountService.isLoggedIn()) {
      const dialogRef = this.dialog.open(LoginDialogComponent);
      this.orderHistoryLoginSub = dialogRef.afterClosed().subscribe({
        next: isLoggedIn => {
          if (!isLoggedIn) return;
          this.router.navigateByUrl('/orders');
          this.sideNavService.toggleSidenav();
        }
      })
    } else {
      this.router.navigateByUrl('/orders');
      this.sideNavService.toggleSidenav();
    }
  }

  onAccount() {
    if (!this.accountService.isLoggedIn()) {
      const dialog = this.dialog.open(LoginDialogComponent);
      this.accountSub = dialog.afterClosed().subscribe({
        next: isLoggedIn => {
          if (!isLoggedIn) return;
          this.router.navigateByUrl('/account');
          this.sideNavService.toggleSidenav();
        }
      })
    } else {
      this.router.navigateByUrl('/account');
      this.sideNavService.toggleSidenav();
    }
  }

  onSelectLanguage() {
    this._bottomSheet.open(LanguageBottomSheetComponent);
    this.sideNavService.toggleSidenav();
  }

  onRestaurants() {
    this.router.navigateByUrl('/home');
    this.sideNavService.toggleSidenav();
  }


  onLogout() {
    if (!this.accountService.isLoggedIn()) {
      this.snackBar.open("You are not logged in.", "Ok");
    } else { 
      this.accountService.logout();
      this.router.navigateByUrl('/home');
      this.sideNavService.toggleSidenav();
      this.snackBar.open("You are successfully logged out.", "Ok");
    }
  }

  ngOnDestroy(): void {
    this.orderHistoryLoginSub?.unsubscribe();
  }

}
