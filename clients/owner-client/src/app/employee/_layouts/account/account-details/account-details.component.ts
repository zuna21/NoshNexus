import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountHeaderComponent } from 'src/app/_components/account-header/account-header.component';
import { IGetAccountDetails } from 'src/app/employee/_interfaces/account.interface';
import { AccountService } from 'src/app/employee/_services/account.service';
import { Subscription } from 'rxjs';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-account-details',
  standalone: true,
  imports: [
    CommonModule,
    AccountHeaderComponent,
    MatDividerModule
  ],
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit, OnDestroy {
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
      next: account => this.account = account
    });
  }


  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
