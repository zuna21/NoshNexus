import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LineWeekOrdersByHourComponent } from 'src/app/_charts/line-week-orders-by-hour/line-week-orders-by-hour.component';

@Component({
  selector: 'app-week-orders-by-hour',
  standalone: true,
  imports: [
    CommonModule,
    LineWeekOrdersByHourComponent
  ],
  templateUrl: './week-orders-by-hour.component.html',
  styleUrls: ['./week-orders-by-hour.component.css']
})
export class WeekOrdersByHourComponent {

}
