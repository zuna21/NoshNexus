import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileHeaderComponent } from 'src/app/_components/profile-header/profile-header.component';
import { MatTabsModule } from '@angular/material/tabs';
import { AccountInfoComponent } from './account-info/account-info.component';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { Router } from '@angular/router';
import { IGetOwner } from 'src/app/_interfaces/IOwner';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    CommonModule,
    ProfileHeaderComponent,
    MatTabsModule,
    AccountInfoComponent,
  ],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})
export class AccountComponent implements OnInit, OnDestroy {
  account: IGetOwner | undefined;

  accountSub: Subscription | undefined;

  constructor(
    private accountService: AccountService,
    private router: Router
    ) {}

  ngOnInit(): void {
    this.getOwner();
  }

  getOwner() {
    this.accountSub = this.accountService.getOwner().subscribe({
      next: (account) => (this.account = account),
    });
  }

  onEditAccount(onEdit: boolean) {
    if (!onEdit) return;
    this.router.navigateByUrl('/account/edit');
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
