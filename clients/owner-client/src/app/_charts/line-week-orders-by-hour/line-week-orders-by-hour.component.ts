import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { IWeekOrderByHour } from 'src/app/_interfaces/IChart';
import { Subscription } from 'rxjs';
import { ChartService } from 'src/app/_services/chart.service';

@Component({
  selector: 'app-line-week-orders-by-hour',
  standalone: true,
  imports: [
    CommonModule,
    NgxChartsModule
  ],
  templateUrl: './line-week-orders-by-hour.component.html',
  styleUrls: ['./line-week-orders-by-hour.component.css']
})
export class LineWeekOrdersByHourComponent implements OnInit, OnDestroy {
    chartData: IWeekOrderByHour[] = [];

    chartDataSub?: Subscription;

    constructor(
      private chartService: ChartService
    ) {}

    // options
    legend: boolean = true;
    showLabels: boolean = true;
    animations: boolean = true;
    xAxis: boolean = true;
    yAxis: boolean = true;
    showYAxisLabel: boolean = true;
    showXAxisLabel: boolean = true;
    xAxisLabel: string = 'Hours';
    yAxisLabel: string = 'Orders';
    timeline: boolean = true;

    ngOnInit(): void {
      this.getChartData();
    }

    getChartData() {
      this.chartDataSub = this.chartService.getWeekOrdersByHour().subscribe({
        next: data => this.chartData = data
      });
    }

    ngOnDestroy(): void {
      this.chartDataSub?.unsubscribe();
    }
}
