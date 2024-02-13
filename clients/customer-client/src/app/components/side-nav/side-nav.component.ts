import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { MatRippleModule } from '@angular/material/core';

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
export class SideNavComponent {

  constructor(
    private accountService: AccountService,
    private router: Router
  ) {}




  onLogout() {
    this.accountService.logout();
    this.router.navigateByUrl('/home');
  }

}
