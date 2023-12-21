import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDrawerMode, MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { SideNavComponent } from './side-nav/side-nav.component';
import { TopNavComponent } from './top-nav/top-nav.component';
import { MatIconModule } from '@angular/material/icon';
import { BreakpointObserver } from '@angular/cdk/layout';
import { Subscription } from 'rxjs';
import { RouterOutlet } from '@angular/router';
import { ChatComponent } from '../chat/chat.component';
import { AccountService } from 'src/app/_services/account.service';
import { ChatHubService } from 'src/app/_services/chat-hub.service';

@Component({
  selector: 'app-main-components',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatSidenavModule,
    MatButtonModule,
    SideNavComponent,
    TopNavComponent,
    RouterOutlet,
    ChatComponent
  ],
  templateUrl: './main-components.component.html',
  styleUrls: ['./main-components.component.css'],
})
export class MainComponentsComponent implements OnInit, OnDestroy {
  hasBackdrop: boolean = false;
  mode: MatDrawerMode = 'side';

  breakPointSub: Subscription | undefined;
  userSub: Subscription | undefined;


  constructor(
    private breakpointObserver: BreakpointObserver,
    private accountService: AccountService,
    private chatHubSevice: ChatHubService
  ) {}

  ngOnInit(): void {
    this.onTabletOrSmallerDevice();
    this.setUser();
    this.connectToChatHub();
  }

  onTabletOrSmallerDevice() {
    this.breakPointSub = this.breakpointObserver
      .observe('(max-width: 960px)')
      .subscribe({
        next: (result) => {
          if (!result) return;
          if (!result.matches) {
            // kada je vece od tableta i mobitela
            this.hasBackdrop = false;
            this.mode = 'side';
          } else {
            // kada je tablet ili mobitel
            this.hasBackdrop = true;
            this.mode = 'over';
          }
        },
      });
  }

  connectToChatHub() {
    const token = this.accountService.getToken();
    if (!token) return;
    this.chatHubSevice.startConnection(token);
  }

  setUser() {
    this.userSub = this.accountService.getUser().subscribe();
  }

  ngOnDestroy(): void {
    this.breakPointSub?.unsubscribe();
    this.userSub?.unsubscribe();
    
    this.chatHubSevice.stopConnection();
  }
}
