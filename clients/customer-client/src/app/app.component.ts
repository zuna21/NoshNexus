import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import {BreakpointObserver, LayoutModule} from '@angular/cdk/layout'; 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, 
    RouterOutlet,
    LayoutModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {

  constructor(
    private translate: TranslateService,
    private breakpointObserver: BreakpointObserver,
    private router: Router
  ) {
    translate.addLangs(['en', 'bs']);
    const selectedLand = localStorage.getItem('lang');
    translate.setDefaultLang(selectedLand ?? 'en')
  }

  ngOnInit(): void {
    this.allowOnlyMobileDevices();
  }


  allowOnlyMobileDevices() {
    const isSmallScreen = this.breakpointObserver.isMatched('(max-width: 599px)');
    if (!isSmallScreen) this.router.navigateByUrl('/desktop');
  }
}
