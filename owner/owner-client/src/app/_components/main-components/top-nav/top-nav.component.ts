import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { SearchBarComponent } from '../../search-bar/search-bar.component';
import { TopNavToggleComponent } from './top-nav-toggle/top-nav-toggle.component';

@Component({
  selector: 'app-top-nav',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    SearchBarComponent,
    TopNavToggleComponent,
  ],
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css'],
})
export class TopNavComponent {}
