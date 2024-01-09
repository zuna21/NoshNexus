import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColumnChartComponent } from 'src/app/_charts/column-chart/column-chart.component';

@Component({
  selector: 'app-order-by-day',
  standalone: true,
  imports: [
    CommonModule,
    ColumnChartComponent
  ],
  templateUrl: './order-by-day.component.html',
  styleUrls: ['./order-by-day.component.css']
})
export class OrderByDayComponent {

}
