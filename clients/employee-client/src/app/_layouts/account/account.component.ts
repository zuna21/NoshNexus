import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { AccountInfoComponent } from './account-info/account-info.component';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { Router } from '@angular/router';
import { IGetOwner } from 'src/app/_interfaces/IOwner';
import { AccountHeaderComponent } from 'src/app/_components/account-header/account-header.component';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    CommonModule,
    AccountHeaderComponent,
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

  onDeleteAccount(onDelete: boolean) {
    if (!onDelete) return;
    console.log('Izbrisi');
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
