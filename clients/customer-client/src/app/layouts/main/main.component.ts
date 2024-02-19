import { Component, ElementRef, OnDestroy, OnInit, ViewChild, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { TopNavComponent } from '../../components/top-nav/top-nav.component';
import { SideNavComponent } from '../../components/side-nav/side-nav.component';
import { ScrollService } from './scroll.service';
import { Subscription } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LoadingService } from '../../services/loading.service';
import { SideNavService } from '../../components/side-nav/side-nav.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    RouterOutlet, 
    MatSidenavModule, 
    TopNavComponent, 
    SideNavComponent,
    MatProgressSpinnerModule,
    AsyncPipe
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent implements OnInit, OnDestroy {
  isLoading = signal<boolean>(false);
  @ViewChild('drawer') drawer?: MatSidenav;

  refreshSub?: Subscription;
  isLoadingSub?: Subscription;
  toggleSub?: Subscription;

  constructor(
    private scrollService: ScrollService,
    private accountService: AccountService,
    public loadingService: LoadingService,
    private sidenavService: SideNavService
  ) { }

  ngOnInit(): void {
    this.loadingFun();
    this.refreshCustomer();
    this.onToggleSidenav();
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

  onToggleSidenav() {
    this.toggleSub = this.sidenavService.$toggleSidenav.subscribe({
      next: _ => {
        if (!this.drawer) return;
        this.drawer.toggle();
      }
    })
  }

  ngOnDestroy(): void {
    this.refreshSub?.unsubscribe();
    this.isLoadingSub?.unsubscribe();
    this.toggleSub?.unsubscribe();
  }
}
