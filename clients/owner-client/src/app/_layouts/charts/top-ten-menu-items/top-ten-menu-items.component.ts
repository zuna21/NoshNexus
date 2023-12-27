import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PieTopTenMenuItemsComponent } from 'src/app/_charts/pie-top-ten-menu-items/pie-top-ten-menu-items.component';

@Component({
  selector: 'app-top-ten-menu-items',
  standalone: true,
  imports: [
    CommonModule,
    PieTopTenMenuItemsComponent
  ],
  templateUrl: './top-ten-menu-items.component.html',
  styleUrls: ['./top-ten-menu-items.component.css']
})
export class TopTenMenuItemsComponent {

}
