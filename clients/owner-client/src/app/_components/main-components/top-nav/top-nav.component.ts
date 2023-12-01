import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { SearchBarComponent } from '../../search-bar/search-bar.component';
import { TopNavButtonsComponent } from './top-nav-buttons/top-nav-buttons.component';

@Component({
  selector: 'app-top-nav',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    SearchBarComponent,
    TopNavButtonsComponent
  ],
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css'],
})
export class TopNavComponent {}
