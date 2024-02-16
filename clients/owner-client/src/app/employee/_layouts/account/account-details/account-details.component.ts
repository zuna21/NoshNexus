import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountHeaderComponent } from 'src/app/_components/account-header/account-header.component';
import { IGetAccountDetails } from 'src/app/employee/_interfaces/account.interface';
import { Subscription } from 'rxjs';
import { MatDividerModule } from '@angular/material/divider';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-account-details',
  standalone: true,
  imports: [
    CommonModule,
    AccountHeaderComponent,
    MatDividerModule,
    TranslateModule
  ],
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit, OnDestroy {
  account?: IGetAccountDetails;

  accountSub?: Subscription;

  constructor(
    private accountService: AccountService,
    private router: Router
  ) {}

  
  ngOnInit(): void {
    this.getAccountDetails();
  }
  

  getAccountDetails() {
    this.accountSub = this.accountService.getEmployee().subscribe({
      next: account => this.account = account
    });
  }

  onEdit() {
    this.router.navigateByUrl('/employee/account-edit');
  }


  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
