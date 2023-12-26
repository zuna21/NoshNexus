import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VerticalBarWeekDayOrdersComponent } from 'src/app/_charts/vertical-bar-week-day-orders/vertical-bar-week-day-orders.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    VerticalBarWeekDayOrdersComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {


}
