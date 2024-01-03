import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IOrderCard } from '../_interfaces/IOrder';

const BASE_URL: string = `${environment.apiUrl}/order`;

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}

  getOwnerInProgressOrders(): Observable<IOrderCard[]> {
    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/owner/orders/get-in-progress-orders`);
  }

  getOrdersHistory(): Observable<IOrderCard[]> {
    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/owner/orders/get-orders-history`);
  }
}
