import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { TopNavComponent } from '../../components/top-nav/top-nav.component';
import { SideNavComponent } from '../../components/side-nav/side-nav.component';
import { ScrollService } from './scroll.service';
import { Subscription } from 'rxjs';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RouterOutlet, MatSidenavModule, TopNavComponent, SideNavComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent implements OnInit, OnDestroy {
  refreshSub?: Subscription;

  constructor(
    private scrollService: ScrollService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.refreshCustomer();
  }

  refreshCustomer() {
    this.refreshSub = this.accountService.refreshCustomer().subscribe();
  }

  onScroll(event: any) {
    // visible height + pixel scrolled >= total height
    if (
      event.target.offsetHeight + event.target.scrollTop >=
      event.target.scrollHeight
    ) {
      this.scrollService.scolledToBottom$.next();
    }
  }

  ngOnDestroy(): void {
    this.refreshSub?.unsubscribe();
  }
}
