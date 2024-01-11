import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { VerticalBarChartComponent } from 'src/app/_components/charts/vertical-bar-chart/vertical-bar-chart.component';
import {MatNativeDateModule} from '@angular/material/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { IOrdersByDayParams } from 'src/app/_interfaces/query_params.interface';
import { FormsModule } from '@angular/forms';
import { ChartService } from 'src/app/_services/chart.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-orders-by-day',
  standalone: true,
  imports: [
    CommonModule,
    VerticalBarChartComponent,
    MatNativeDateModule,
    MatDatepickerModule,
    MatFormFieldModule,
    FormsModule
  ],
  providers: [
    DatePipe
  ],
  templateUrl: './orders-by-day.component.html',
  styleUrls: ['./orders-by-day.component.css']
})
export class OrdersByDayComponent implements OnInit, OnDestroy {
    restaurantId?: number;
    startDate: Date = new Date();
    endDate: Date = new Date();
    orderByDayParams?: IOrdersByDayParams;
    chartData: number[] = [];

    dataSub?: Subscription;

    constructor(
      private datePipe: DatePipe,
      private router: Router,
      private activatedRoute: ActivatedRoute,
      private chartService: ChartService
    ) {}

    ngOnInit(): void {
      this.setInitDate();
      this.getData();
    }

    getData() {
      this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
      if (!this.restaurantId) return;
      this.dataSub = this.chartService.getOrdersByDay(this.restaurantId).subscribe({
        next: data => this.chartData = [...data]
      });
    }

    setInitDate() {
      this.startDate.setDate(this.endDate.getDate() - 7);
      this.orderByDayParams = {
        ...this.orderByDayParams,
        startDate: this.datePipe.transform(this.startDate, 'dd-MM-yyyy')!,
        endDate: this.datePipe.transform(this.endDate, 'dd-MM-yyyy')!
      }

      this.setQueryParams();
    }

    setQueryParams() {
      if (!this.orderByDayParams) return;
      const queryParams: Params = { ...this.orderByDayParams };
    
      this.router.navigate(
        [], 
        {
          relativeTo: this.activatedRoute,
          queryParams,
        }
      );
    }

    onChangeDate() {
      if (!this.startDate || !this.endDate) return;
      this.orderByDayParams = {
        ...this.orderByDayParams,
        startDate: this.datePipe.transform(this.startDate, 'dd-MM-yyyy')!,
        endDate: this.datePipe.transform(this.endDate, 'dd-MM-yyyy')!
      };
      this.setQueryParams();

      this.chartData = [...[1, 2, 3, 4, 5, 6, 7]]
    }

    ngOnDestroy(): void {
      this.dataSub?.unsubscribe();
    }

}
