import { AfterViewInit, Component, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective, NgChartsModule } from 'ng2-charts';
import {
  ChartData,
  ChartOptions,
} from 'chart.js';

@Component({
  selector: 'app-vertical-bar-chart',
  standalone: true,
  imports: [CommonModule, NgChartsModule],
  templateUrl: './vertical-bar-chart.component.html',
  styleUrls: ['./vertical-bar-chart.component.css'],
})
export class VerticalBarChartComponent implements AfterViewInit {
  @ViewChild(BaseChartDirective)
    public chart?: BaseChartDirective;
  @Input('data') set setData(value: number[]) {
    this.data = [...value];
    this.updateChart();
  }
  @Input('labels') set setLabels(value: string[]) {
    this.labels = [...value];
    this.updateChart();
  }

  data: number[] = [65, 59, 80, 81, 56, 55, 40];
  labels: string[] = [
    'ponedeljak',
    'Utorak',
    'Srijeda',
    'Cetvrtak',
    'Petak',
    'Subota',
    'Nedelja',
  ];

  chartData: ChartData = {
    labels: this.labels,
    datasets: [
      {
        label: 'Narudzbe',
        data: this.data,
        backgroundColor: [
          'rgba(255, 99, 132, 0.2)',
          'rgba(255, 159, 64, 0.2)',
          'rgba(255, 205, 86, 0.2)',
          'rgba(75, 192, 192, 0.2)',
          'rgba(54, 162, 235, 0.2)',
          'rgba(153, 102, 255, 0.2)',
          'rgba(201, 203, 207, 0.2)',
        ],
        borderColor: [
          'rgb(255, 99, 132)',
          'rgb(255, 159, 64)',
          'rgb(255, 205, 86)',
          'rgb(75, 192, 192)',
          'rgb(54, 162, 235)',
          'rgb(153, 102, 255)',
          'rgb(201, 203, 207)',
        ],
        borderWidth: 1,
      },
    ],
  };

  options: ChartOptions = {
    scales: {
      y: {
        beginAtZero: true,
      },
    },
    responsive: true,
  };

  ngAfterViewInit(): void {
    this.updateChart();
  }

  updateChart() {
    if (!this.chart) return;
    this.chartData.datasets[0].data = [...this.data];
    this.chartData.labels = [...this.labels];
    this.chart.update();
  }
}
