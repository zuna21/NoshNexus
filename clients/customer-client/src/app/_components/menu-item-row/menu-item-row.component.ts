import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatExpansionModule} from '@angular/material/expansion'; 
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-menu-item-row',
  standalone: true,
  imports: [
    CommonModule,
    MatExpansionModule,
    MatButtonModule
  ],
  templateUrl: './menu-item-row.component.html',
  styleUrls: ['./menu-item-row.component.css']
})
export class MenuItemRowComponent {

}
