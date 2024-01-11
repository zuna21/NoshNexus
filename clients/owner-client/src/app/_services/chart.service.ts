import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOrdersByDayParams } from '../_interfaces/query_params.interface';

@Injectable({
  providedIn: 'root',
})
export class ChartService {
  constructor(private http: HttpClient) {}

  getOrdersByDay(restaurantId: number, ordersByDayQueryParams: IOrdersByDayParams): Observable<number[]> {
    let params = new HttpParams();
    params = params.set('startDate', ordersByDayQueryParams.startDate);
    params = params.set('endDate', ordersByDayQueryParams.endDate);

    return this.http.get<number[]>(
      `http://localhost:5000/api/owner/charts/get-orders-by-day/${restaurantId}`, { params }
    );
  }
}
