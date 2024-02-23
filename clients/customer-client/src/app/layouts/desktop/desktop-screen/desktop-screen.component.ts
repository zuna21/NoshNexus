import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-desktop-screen',
  standalone: true,
  imports: [
    TranslateModule
  ],
  templateUrl: './desktop-screen.component.html',
  styleUrl: './desktop-screen.component.css'
})
export class DesktopScreenComponent implements OnInit {

  constructor(
    private breakpointObserver: BreakpointObserver,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.navigateIfMobile();
  }

  navigateIfMobile() {
    const isSmallScreen = this.breakpointObserver.isMatched('(max-width: 599px)');
    if (isSmallScreen) this.router.navigateByUrl('/home');
  }
}
