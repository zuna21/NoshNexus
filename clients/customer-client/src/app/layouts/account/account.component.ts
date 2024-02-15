import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { IGetAccountDetails } from '../../interfaces/account.interface';
import { AccountService } from '../../services/account.service';
import { Subscription } from 'rxjs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    DatePipe,
    MatIconModule,
    MatButtonModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css'
})
export class AccountComponent implements OnInit, OnDestroy {
  isProfileLoading = signal<boolean>(true);
  account?: IGetAccountDetails;

  accountSub?: Subscription;

  constructor(
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getAccountDetails();
  }

  getAccountDetails() {
    this.accountSub = this.accountService.getAccountDetails().subscribe({
      next: account => {
        if (!account) return;
        this.account = account;
        console.log(this.account);
      }
    });
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
