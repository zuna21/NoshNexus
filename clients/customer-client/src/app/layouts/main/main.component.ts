import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { TopNavComponent } from '../../components/top-nav/top-nav.component';
import { SideNavComponent } from '../../components/side-nav/side-nav.component';
import { ScrollService } from './scroll.service';
import { Subscription } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    RouterOutlet, 
    MatSidenavModule, 
    TopNavComponent, 
    SideNavComponent,
    MatProgressSpinnerModule,
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent implements OnInit, OnDestroy {
  isLoading = signal<boolean>(false);

  refreshSub?: Subscription;
  isLoadingSub?: Subscription;

  constructor(
    private scrollService: ScrollService,
    private accountService: AccountService,
    public loadingService: LoadingService
  ) { }

  ngOnInit(): void {
    this.loadingFun();
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

  loadingFun() {
    this.isLoadingSub = this.loadingService.isLoading$.subscribe({
      next: isLoading => {
        setTimeout(() => {
          this.isLoading.set(isLoading);
        }, 0);
      }
    });
  }

  ngOnDestroy(): void {
    this.refreshSub?.unsubscribe();
    this.isLoadingSub?.unsubscribe();
  }
}
