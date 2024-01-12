import { AfterViewInit, Component, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective, NgChartsModule } from 'ng2-charts';
import { ChartConfiguration, ChartType } from 'chart.js';
import { ILineChart } from 'src/app/_interfaces/IChart';

@Component({
  selector: 'app-line-chart',
  standalone: true,
  imports: [CommonModule, NgChartsModule],
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css'],
})
export class LineChartComponent implements AfterViewInit {
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
  @Input('chartData') set setChartData(value: ILineChart) {
    this.chartData = {...value};
    this.updateChart();
  }

  chartData: ILineChart = {
    labels: [
      '07:00',
      '08:00',
      '09:00',
      '10:00',
      '11:00',
      '12:00',
      '13:00',
      '14:00',
      '15:00',
      '16:00',
      '17:00',
      '18:00',
      '19:00',
      '20:00',
      '21:00',
      '22:00',
      '23:00',
      '00:00',
    ],
    data: [
      180, 480, 770, 90, 1000, 270, 400, 323, 523, 242, 553, 233, 131, 553, 533,
      555, 232, 665,
    ],
  };

  public lineChartData: ChartConfiguration['data'] = {
    datasets: [
      {
        data: this.chartData.data,
        label: 'Series C',
        yAxisID: 'y1',
        backgroundColor: 'rgba(245, 124, 0, 0.3)',
        borderColor: '#F57C00',
        pointBackgroundColor: 'rgba(148,159,177,1)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(148,159,177,0.8)',
        fill: 'origin',
      },
    ],
    labels: this.chartData.labels,
  };

  public lineChartType: ChartType = 'line';

  public lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    elements: {
      line: {
        tension: 0.5,
      },
    },
    scales: {
      // We use this empty structure as a placeholder for dynamic theming.
      y: {
        position: 'left',
      },
      y1: {
        position: 'right',
        grid: {
          color: 'rgba(245, 124, 0, 0.3)',
        },
        ticks: {
          color: '#F57C00',
        },
      },
    },

    plugins: {
      legend: { display: true },
    },
  };

  ngAfterViewInit(): void {
    this.updateChart();
  }

  updateChart() {
    if (!this.chart) return;
    this.lineChartData.datasets[0].data = [...this.chartData.data];
    this.lineChartData.labels = [...this.chartData.labels];
    this.chart.update();
  }
}
