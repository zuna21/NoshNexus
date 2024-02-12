import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatSidenavModule} from '@angular/material/sidenav';
import { TopNavComponent } from '../../components/top-nav/top-nav.component';
import { SideNavComponent } from '../../components/side-nav/side-nav.component';
import { ScrollService } from './scroll.service';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    RouterOutlet,
    MatSidenavModule,
    TopNavComponent,
    SideNavComponent
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

  constructor(
    private scrollService: ScrollService
  ) {}

  onScroll(event: any) {
    // visible height + pixel scrolled >= total height 
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
      this.scrollService.scolledToBottom$.next();
    }
}
}
