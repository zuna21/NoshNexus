import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { VerticalBarChartComponent } from 'src/app/_components/charts/vertical-bar-chart/vertical-bar-chart.component';
import { IVerticalBarChart } from 'src/app/_interfaces/IChart';
import { Subscription, mergeMap } from 'rxjs';
import { ChartService } from 'src/app/_services/chart.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {MatNativeDateModule} from '@angular/material/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { IOrdersByDayParams } from 'src/app/_interfaces/query_params.interface';

@Component({
  selector: 'app-orders-by-day',
  standalone: true,
  imports: [
    CommonModule,
    VerticalBarChartComponent,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    FormsModule,
  ],
  providers: [DatePipe],
  templateUrl: './orders-by-day.component.html',
  styleUrls: ['./orders-by-day.component.css']
})
export class OrdersByDayComponent implements OnInit, OnDestroy {
  data: IVerticalBarChart[] = [];
  restaurantId?: number;
  startDate = new Date();
  endDate = new Date();
  ordersByDayQueryParams: IOrdersByDayParams = {
    endDate: '',
    startDate: ''
  };

  dataSub?: Subscription;

  constructor(
    private chartService: ChartService,
    private activatedRoute: ActivatedRoute,
    private datePipe: DatePipe,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initDate();
    this.getData();
  }

  initDate() {
    this.startDate.setDate(this.endDate.getDate() - 7);

    if (!this.startDate || !this.endDate) return;

    this.ordersByDayQueryParams = {
      ...this.ordersByDayQueryParams,
      startDate: this.datePipe.transform(this.startDate, 'dd-MM-yyyy')!,
      endDate: this.datePipe.transform(this.endDate, 'dd-MM-yyyy')!,
    }

    this.setQueryParams();
    
  }

  setQueryParams() {
    const queryParams: Params = { ...this.ordersByDayQueryParams };
    this.router.navigate(
      [], 
      {
        relativeTo: this.activatedRoute,
        queryParams
      }
    );
  }

  getData() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);

    if (!this.restaurantId || !this.startDate || !this.endDate) return;
    this.dataSub = this.activatedRoute.queryParams.pipe(
      mergeMap(_ => this.chartService.getOrdersByDay(this.restaurantId!))
    ).subscribe({
      next: data => this.data = [...data]
    });

  }

  onSelectedDate() {
    if (!this.startDate || !this.endDate) return;

    this.ordersByDayQueryParams = {
      ...this.ordersByDayQueryParams,
      startDate: this.datePipe.transform(this.startDate, 'dd-MM-yyyy')!,
      endDate: this.datePipe.transform(this.endDate, 'dd-MM-yyyy')!,
    }

    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.dataSub?.unsubscribe();
  }

}
