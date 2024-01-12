import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PieChartComponent } from 'src/app/_components/charts/pie-chart/pie-chart.component';
import { IPieChart } from 'src/app/_interfaces/IChart';

@Component({
  selector: 'app-top-ten-menu-items',
  standalone: true,
  imports: [
    CommonModule,
    PieChartComponent
  ],
  templateUrl: './top-ten-menu-items.component.html',
  styleUrls: ['./top-ten-menu-items.component.css']
})
export class TopTenMenuItemsComponent {
  chartData: IPieChart = {
    labels: ["prvi", "drugi", "treci", "cetrvti", "peti", "sesti", "sedmi", "osmi", "deveti", "deseti"],
    data: [21, 22, 41, 32, 11, 43,21, 24,44, 39],
  }
}
