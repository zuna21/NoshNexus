import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { MatTabsModule } from '@angular/material/tabs';
import { MenuItemCreateComponent } from '../menu-item-create/menu-item-create.component';

@Component({
  selector: 'app-menus-details',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatTabsModule,
    MenuItemCreateComponent,
  ],
  templateUrl: './menus-details.component.html',
  styleUrls: ['./menus-details.component.css'],
})
export class MenusDetailsComponent {}
