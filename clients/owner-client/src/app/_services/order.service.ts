import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IOrderCard } from '../_interfaces/IOrder';
import { IOrdersHistoryQueryParams } from '../_interfaces/query_params.interface';

const BASE_URL: string = `${environment.apiUrl}/order`;

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}

  getOwnerInProgressOrders(): Observable<IOrderCard[]> {
    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/owner/orders/get-in-progress-orders`);
  }

  getOrdersHistory(ordersHistoryQueryParams: IOrdersHistoryQueryParams): Observable<IOrderCard[]> {
    let params = new HttpParams();

    params = params.set('status', ordersHistoryQueryParams.status);

    if (ordersHistoryQueryParams.restaurant) params = params.set('restaurant', ordersHistoryQueryParams.restaurant);
    if (ordersHistoryQueryParams.search) params = params.set('search', ordersHistoryQueryParams.search);

    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/owner/orders/get-orders-history`, { params });
  }
}
