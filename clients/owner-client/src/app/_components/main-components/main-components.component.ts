import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
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
import { LoadingComponent } from 'src/app/_layouts/loading/loading.component';
import { LoadingService } from 'src/app/_services/loading.service';

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
    ChatComponent,
    LoadingComponent
  ],
  templateUrl: './main-components.component.html',
  styleUrls: ['./main-components.component.css'],
})
export class MainComponentsComponent implements OnInit, OnDestroy {
  hasBackdrop: boolean = false;
  mode: MatDrawerMode = 'side';
  isLoading: boolean = false;

  breakPointSub?: Subscription;
  userSub?: Subscription;
  isLoadingSub?: Subscription;

  constructor(
    private breakpointObserver: BreakpointObserver,
    private accountService: AccountService,
    private chatHubSevice: ChatHubService,
    private loadingService: LoadingService
  ) {}

  async ngOnInit(): Promise<void> {
    this.loadingFun();
    this.onTabletOrSmallerDevice();
    this.setUser();

    await this.connectToChatHub();
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

  async connectToChatHub() {
    const token = this.accountService.getToken();
    if (!token) return;
    await this.chatHubSevice.startConnection(token);
  }

  setUser() {
    this.userSub = this.accountService.getUser().subscribe();
  }

  // Mora biti setTimeout() da se funkcija zadnja izvrsi
  loadingFun() {
    this.isLoadingSub = this.loadingService.isLoading$.subscribe({
      next: isLoading => {
        setTimeout(() => {
          this.isLoading = isLoading;
        }, 0);
      }
    });
  }


  ngOnDestroy(): void {
    this.breakPointSub?.unsubscribe();
    this.userSub?.unsubscribe();
    this.isLoadingSub?.unsubscribe();

    this.chatHubSevice.stopConnection();
  }
}
