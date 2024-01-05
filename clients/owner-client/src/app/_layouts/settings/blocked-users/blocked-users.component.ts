import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserCardComponent } from 'src/app/_components/user-card/user-card.component';
import { IUserCard } from 'src/app/_interfaces/IUser';
import { Subscription } from 'rxjs';
import { SettingService } from 'src/app/_services/setting.service';

@Component({
  selector: 'app-blocked-users',
  standalone: true,
  imports: [
    CommonModule,
    UserCardComponent
  ],
  templateUrl: './blocked-users.component.html',
  styleUrls: ['./blocked-users.component.css']
})
export class BlockedUsersComponent implements OnInit, OnDestroy {
  blockedUsers: IUserCard[] = [];

  blockedUserSub?: Subscription;

  constructor(
    private settingService: SettingService
  ) {}

  ngOnInit(): void {
    this.getBlockedCustomers();
  }

  getBlockedCustomers() {
    this.blockedUserSub = this.settingService.getBlockedCustomers().subscribe({
      next: users => this.blockedUsers = [...users]
    });
  }

  ngOnDestroy(): void {
    this.blockedUserSub?.unsubscribe();
  }
}
