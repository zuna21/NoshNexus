import { Component, OnDestroy } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { MatRippleModule } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    MatIconModule,
    RouterLink,
    MatRippleModule
  ],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.css'
})
export class SideNavComponent implements OnDestroy {

  orderHistoryLoginSub?: Subscription;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private dialog: MatDialog
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


  onLogout() {
    this.accountService.logout();
    this.router.navigateByUrl('/home');
  }

  ngOnDestroy(): void {
    this.orderHistoryLoginSub?.unsubscribe();
  }

}
