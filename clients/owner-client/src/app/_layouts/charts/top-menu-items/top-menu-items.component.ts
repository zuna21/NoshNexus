import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PieChartComponent } from 'src/app/_charts/pie-chart/pie-chart.component';

@Component({
  selector: 'app-top-menu-items',
  standalone: true,
  imports: [
    CommonModule,
    PieChartComponent
  ],
  templateUrl: './top-menu-items.component.html',
  styleUrls: ['./top-menu-items.component.css']
})
export class TopMenuItemsComponent {
  
}
