import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnerSideNavComponent } from './owner-side-nav/owner-side-nav.component';
import { AccountService } from 'src/app/_services/account.service';
import { EmployeeSideNavComponent } from './employee-side-nav/employee-side-nav.component';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    CommonModule,
    OwnerSideNavComponent,
    EmployeeSideNavComponent
  ],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css'],
})
export class SideNavComponent implements OnInit {
  isOwner: boolean = false;

  constructor(
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.isOwner = this.accountService.getRole() === 'owner';
  }
}
