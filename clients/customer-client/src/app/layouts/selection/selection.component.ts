import { Component } from '@angular/core';

import {MatTabsModule} from '@angular/material/tabs';
import { MenuItemsComponent } from '../menu-items/menu-items.component';
import { MenusComponent } from '../menus/menus.component';


@Component({
  selector: 'app-selection',
  standalone: true,
  imports: [
    MatTabsModule,
    MenuItemsComponent,
    MenusComponent,
  ],
  templateUrl: './selection.component.html',
  styleUrl: './selection.component.css'
})
export class SelectionComponent {

}
