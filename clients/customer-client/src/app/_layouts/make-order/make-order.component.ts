import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatTabsModule} from '@angular/material/tabs';
import { MenuItemsComponent } from './menu-items/menu-items.component';
import { MenusComponent } from './menus/menus.component';

@Component({
  selector: 'app-make-order',
  standalone: true,
  imports: [
    CommonModule,
    MatTabsModule,
    MenuItemsComponent,
    MenusComponent
  ],
  templateUrl: './make-order.component.html',
  styleUrls: ['./make-order.component.css']
})
export class MakeOrderComponent {

}
