import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWeekDayOrder } from '../_interfaces/IChart';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  constructor(
    private http: HttpClient
  ) { }

  getWeekDayOrders(): Observable<IWeekDayOrder[]> {
    return this.http.get<IWeekDayOrder[]>(`http://localhost:3000/charts/get-week-day-orders`);
  }
}
