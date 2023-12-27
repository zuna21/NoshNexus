import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IChartCard } from 'src/app/_interfaces/IChart';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-chart-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './chart-card.component.html',
  styleUrls: ['./chart-card.component.css']
})
export class ChartCardComponent {
  @Input('chart') chart?: IChartCard;
  @Output('viewChart') viewChart = new EventEmitter<number>();

  isImageLoading: boolean = true;

  onViewChart() {
    if (!this.chart) return;
    this.viewChart.emit(this.chart.id);
  }
}
