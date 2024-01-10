import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VerticalBarChartComponent } from 'src/app/_components/charts/vertical-bar-chart/vertical-bar-chart.component';

@Component({
  selector: 'app-orders-by-day',
  standalone: true,
  imports: [
    CommonModule,
    VerticalBarChartComponent
  ],
  templateUrl: './orders-by-day.component.html',
  styleUrls: ['./orders-by-day.component.css']
})
export class OrdersByDayComponent {

}
