import { AfterViewInit, Component, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective, NgChartsModule } from 'ng2-charts';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { IPieChart } from 'src/app/_interfaces/IChart';

@Component({
  selector: 'app-pie-chart',
  standalone: true,
  imports: [
    CommonModule,
    NgChartsModule
  ],
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements AfterViewInit {
  @ViewChild(BaseChartDirective)
    public chart?: BaseChartDirective;
    
  @Input('chartData') set setChartData(value: IPieChart) {
    this.chartData = {
      ...value,
      data: [...value.data],
      labels: [...value.labels]
    };
    this.updateChart();
  }

  chartData: IPieChart = {
    labels: ["prvi", "drugi", "treci", "cetrvti", "peti", "sesti", "sedmi", "osmi", "deveti", "deseti"],
    data: [21, 22, 41, 32, 11, 43,21, 24,44, 39],
  }

  pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: this.chartData.labels,
    datasets: [
      {
        data: this.chartData.data
      },
    ],
  };

  pieChartType: ChartType = 'pie';
  pieChartOptions: ChartConfiguration['options'] = {
    plugins: {
      legend: {
        display: true,
        position: 'right',
      },
    },
    responsive: true
  };

  ngAfterViewInit(): void {
    this.updateChart();
  }

  updateChart() {
    if (!this.chart) return;
    this.pieChartData.labels = [...this.chartData.labels];
    this.pieChartData.datasets[0].data = [...this.chartData.data];
    this.chart.update();
  }
}
