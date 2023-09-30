import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { SideNavComponent } from './side-nav/side-nav.component';
import { TopNavComponent } from './top-nav/top-nav.component';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-main-components',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatSidenavModule, MatButtonModule, SideNavComponent, TopNavComponent],
  templateUrl: './main-components.component.html',
  styleUrls: ['./main-components.component.css']
})
export class MainComponentsComponent {

}
