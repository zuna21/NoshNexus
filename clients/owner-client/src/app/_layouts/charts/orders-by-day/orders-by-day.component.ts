import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VerticalBarChartComponent } from 'src/app/_components/charts/vertical-bar-chart/vertical-bar-chart.component';
import { IVerticalBarChart } from 'src/app/_interfaces/IChart';
import { Subscription } from 'rxjs';
import { ChartService } from 'src/app/_services/chart.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-orders-by-day',
  standalone: true,
  imports: [
    CommonModule,
    VerticalBarChartComponent
  ],
  templateUrl: './orders-by-day.component.html',
  styleUrls: ['./orders-by-day.component.css']
})
export class OrdersByDayComponent implements OnInit, OnDestroy {
  data: IVerticalBarChart[] = [];
  restaurantId?: number;

  dataSub?: Subscription;

  constructor(
    private chartService: ChartService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;
    this.dataSub = this.chartService.getOrdersByDay(this.restaurantId).subscribe({
      next: data => this.data = [...data]
    });
  }

  ngOnDestroy(): void {
    this.dataSub?.unsubscribe();
  }

}
