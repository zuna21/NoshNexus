import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VerticalBarWeekDayOrdersComponent } from 'src/app/_charts/vertical-bar-week-day-orders/vertical-bar-week-day-orders.component';

@Component({
  selector: 'app-week-day-orders',
  standalone: true,
  imports: [
    CommonModule,
    VerticalBarWeekDayOrdersComponent
  ],
  templateUrl: './week-day-orders.component.html',
  styleUrls: ['./week-day-orders.component.css']
})
export class WeekDayOrdersComponent {

}
