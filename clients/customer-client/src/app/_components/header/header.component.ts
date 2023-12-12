import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatIconModule} from '@angular/material/icon'; 
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';  
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  @Output('openSidenavEmitter') openSidenavEmitter = new EventEmitter<boolean>();

  constructor(
    private router: Router,
    private accountService: AccountService
  ) {}

  onOpenSidenav() {
    this.openSidenavEmitter.emit(true);
  }

  onLogout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }

}
