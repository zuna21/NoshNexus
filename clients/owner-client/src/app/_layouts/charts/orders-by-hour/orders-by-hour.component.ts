import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LineChartComponent } from 'src/app/_components/charts/line-chart/line-chart.component';

@Component({
  selector: 'app-orders-by-hour',
  standalone: true,
  imports: [
    CommonModule,
    LineChartComponent
  ],
  templateUrl: './orders-by-hour.component.html',
  styleUrls: ['./orders-by-hour.component.css']
})
export class OrdersByHourComponent {

}
