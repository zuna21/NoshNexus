import { Component, OnDestroy } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { MatRippleModule } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateModule } from '@ngx-translate/core';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    MatIconModule,
    RouterLink,
    MatRippleModule,
    TranslateModule,
    TitleCasePipe
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
    private snackBar: MatSnackBar
  ) {}

  
  onViewOrderHistory() {
    if (!this.accountService.isLoggedIn()) {
      const dialogRef = this.dialog.open(LoginDialogComponent);
      this.orderHistoryLoginSub = dialogRef.afterClosed().subscribe({
        next: isLoggedIn => {
          if (!isLoggedIn) return;
          this.router.navigateByUrl('/orders');
        }
      })
    } else {
      this.router.navigateByUrl('/orders');
    }
  }

  onAccount() {
    if (!this.accountService.isLoggedIn()) {
      const dialog = this.dialog.open(LoginDialogComponent);
      this.accountSub = dialog.afterClosed().subscribe({
        next: isLoggedIn => {
          if (!isLoggedIn) return;
          this.router.navigateByUrl('/account');
        }
      })
    } else {
      this.router.navigateByUrl('/account');
    }
  }


  onLogout() {
    if (!this.accountService.isLoggedIn()) {
      this.snackBar.open("You are not logged in.", "Ok");
    } else { 
      this.accountService.logout();
      this.router.navigateByUrl('/home');
      this.snackBar.open("You are successfully logged out.", "Ok");
    }
  }

  ngOnDestroy(): void {
    this.orderHistoryLoginSub?.unsubscribe();
  }

}
