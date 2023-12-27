import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { ITopTenMenuItem } from 'src/app/_interfaces/IChart';
import { ChartService } from 'src/app/_services/chart.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-pie-top-ten-menu-items',
  standalone: true,
  imports: [CommonModule, NgxChartsModule],
  templateUrl: './pie-top-ten-menu-items.component.html',
  styleUrls: ['./pie-top-ten-menu-items.component.css'],
})
export class PieTopTenMenuItemsComponent implements OnInit, OnDestroy {
  chartData: ITopTenMenuItem[] = [];

  // options
  gradient: boolean = true;
  showLegend: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  legendPosition: any = 'right';

  chartDataSub?: Subscription;

  constructor(
    private chartService: ChartService
  ) {}

  ngOnInit(): void {
    this.getChartData();
  }

  getChartData() {
    this.chartDataSub = this.chartService.getTopTenMenuItems().subscribe({
      next: data => this.chartData = data
    });
  }


  ngOnDestroy(): void {
    this.chartDataSub?.unsubscribe();
  }
}
