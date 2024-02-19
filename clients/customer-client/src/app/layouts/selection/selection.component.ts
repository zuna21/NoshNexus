import { Component } from '@angular/core';

import {MatTabsModule} from '@angular/material/tabs';
import { MenuItemsComponent } from '../menu-items/menu-items.component';
import { MenusComponent } from '../menus/menus.component';
import { OrderBottomNavigationComponent } from '../../components/order-bottom-navigation/order-bottom-navigation.component';
import { TranslateModule } from '@ngx-translate/core';
import { TitleCasePipe } from '@angular/common';


@Component({
  selector: 'app-selection',
  standalone: true,
  imports: [
    MatTabsModule,
    MenuItemsComponent,
    MenusComponent,
    OrderBottomNavigationComponent,
    TranslateModule,
    TitleCasePipe
  ],
  templateUrl: './selection.component.html',
  styleUrl: './selection.component.css'
})
export class SelectionComponent {

}
