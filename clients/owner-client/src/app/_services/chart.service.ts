import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IVerticalBarChart } from '../_interfaces/IChart';

@Injectable({
  providedIn: 'root',
})
export class ChartService {
  constructor(private http: HttpClient) {}

  getOrdersByDay(restaurantId: number): Observable<IVerticalBarChart[]> {
    return this.http.get<IVerticalBarChart[]>(
      `http://localhost:5000/api/owner/charts/get-orders-by-day/${restaurantId}`
    );
  }
}
