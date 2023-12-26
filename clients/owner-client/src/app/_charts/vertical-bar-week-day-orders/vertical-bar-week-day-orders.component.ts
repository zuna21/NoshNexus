import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartService } from 'src/app/_services/chart.service';
import { IWeekDayOrder } from 'src/app/_interfaces/IChart';
import { Subscription } from 'rxjs';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-vertical-bar-week-day-orders',
  standalone: true,
  imports: [
    CommonModule,
    NgxChartsModule
  ],
  templateUrl: './vertical-bar-week-day-orders.component.html',
  styleUrls: ['./vertical-bar-week-day-orders.component.css']
})
export class VerticalBarWeekDayOrdersComponent implements OnInit, OnDestroy {
  chartData: IWeekDayOrder[] = [];

  chartDataSub?: Subscription;

  view: [number, number] = [700, 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Dani';
  showYAxisLabel = true;
  yAxisLabel = 'Narudzbe';

  constructor(
    private chartService: ChartService
  ) {}


  ngOnInit(): void {
    this.getChartData();
  }

  getChartData() {
    this.chartDataSub = this.chartService.getWeekDayOrders().subscribe({
      next: data => this.chartData = data
    });
  }

  onSelect(event: Event) {
    console.log(event);
  }

  ngOnDestroy(): void {
    this.chartDataSub?.unsubscribe();
  }

}
