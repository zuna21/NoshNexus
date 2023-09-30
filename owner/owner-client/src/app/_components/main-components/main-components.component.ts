import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDrawerMode, MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { SideNavComponent } from './side-nav/side-nav.component';
import { TopNavComponent } from './top-nav/top-nav.component';
import {MatIconModule} from '@angular/material/icon';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-main-components',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatSidenavModule, MatButtonModule, SideNavComponent, TopNavComponent],
  templateUrl: './main-components.component.html',
  styleUrls: ['./main-components.component.css']
})
export class MainComponentsComponent implements OnInit, OnDestroy {
  hasBackdrop: boolean = false;
  mode: MatDrawerMode = 'side';
  sideOpened: boolean = true;

  breakPointSub: Subscription | undefined;

  constructor(private breakpointObserver: BreakpointObserver) {}

  ngOnInit(): void {
    this.onTabletOrSmallerDevice();
  }

  onTabletOrSmallerDevice() {
    this.breakPointSub = this.breakpointObserver.observe('(max-width: 768px)').subscribe({
      next: result => {
        if (!result) return;
        if (!result.matches) {
          // kada je vece od tableta i mobitela
          this.hasBackdrop = false;
          this.mode = 'side';
          this.sideOpened = true;
        } else {
          // kada je tablet ili mobitel
          this.hasBackdrop = true;
          this.mode = 'over';
          this.sideOpened = false;
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.breakPointSub?.unsubscribe();
  }

}
