import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ChartService {
  constructor(private http: HttpClient) {}

  getOrdersByDay(restaurantId: number): Observable<number[]> {
    return this.http.get<number[]>(
      `http://localhost:5000/api/owner/charts/get-orders-by-day/${restaurantId}`
    );
  }
}
