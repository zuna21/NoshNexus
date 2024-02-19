import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { IGetAccountDetails } from '../../interfaces/account.interface';
import { AccountService } from '../../services/account.service';
import { Subscription } from 'rxjs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ActivateAccountDialogComponent } from '../../components/activate-account-dialog/activate-account-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    MatIconModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    TranslateModule,
    TitleCasePipe
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css',
})
export class AccountComponent implements OnInit, OnDestroy {
  isProfileLoading = signal<boolean>(true);
  account?: IGetAccountDetails;

  accountSub?: Subscription;
  activateSub?: Subscription;

  constructor(
    private accountService: AccountService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.getAccountDetails();
  }

  getAccountDetails() {
    this.accountSub = this.accountService.getAccountDetails().subscribe({
      next: (account) => {
        if (!account) return;
        this.account = account;
        console.log(this.account);
      },
    });
  }

  onActivateAccount() {
    if (!this.account || this.account.isActivated) return;
    const dialogRef = this.dialog.open(ActivateAccountDialogComponent);
    this.activateSub = dialogRef.afterClosed().subscribe({
      next: (afterClose: {isActivated: boolean, username: string | null}) => {
        if (afterClose?.isActivated && this.account) {
          this.account = {
            ...this.account,
            isActivated: afterClose.isActivated,
            username: afterClose.username!
          };
          this.snackBar.open('Successfully activated account.', 'Ok');
        } else {
          this.snackBar.open('Please activate you account.', 'Ok');
        }
      },
    });
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
    this.activateSub?.unsubscribe();
  }
}
