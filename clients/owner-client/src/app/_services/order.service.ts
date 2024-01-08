import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IOrderCard } from '../_interfaces/IOrder';
import { IOrdersHistoryQueryParams, IOrdersQueryParams } from '../_interfaces/query_params.interface';
import { IPagedList } from '../_interfaces/IPagedList';

const BASE_URL: string = `${environment.apiUrl}/order`;

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}

  getOwnerInProgressOrders(ordersQueryParams: IOrdersQueryParams): Observable<IOrderCard[]> {
    let params = new HttpParams();

    if (ordersQueryParams.restaurant) params = params.set('restaurant', ordersQueryParams.restaurant);
    if (ordersQueryParams.search) params = params.set('search', ordersQueryParams.search);

    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/owner/orders/get-in-progress-orders`, { params });
  }

  getOrdersHistory(ordersHistoryQueryParams: IOrdersHistoryQueryParams): Observable<IPagedList<IOrderCard[]>> {
    let params = new HttpParams();

    params = params.set('pageIndex', ordersHistoryQueryParams.pageIndex);
    params = params.set('status', ordersHistoryQueryParams.status);

    if (ordersHistoryQueryParams.restaurant) params = params.set('restaurant', ordersHistoryQueryParams.restaurant);
    if (ordersHistoryQueryParams.search) params = params.set('search', ordersHistoryQueryParams.search);

    return this.http.get<IPagedList<IOrderCard[]>>(`http://localhost:5000/api/owner/orders/get-orders-history`, { params });
  }

  blockCustomer(orderId: number): Observable<number> {
    return this.http.get<number>(`http://localhost:5000/api/owner/orders/block-customer/${orderId}`);
  }

  accept(orderId: number): Observable<number> {
    return this.http.get<number>(`http://localhost:5000/api/owner/orders/accept-order/${orderId}`);
  }

  decline(orderId: number, reason: string): Observable<number> {
    return this.http.put<number>(`http://localhost:5000/api/owner/orders/decline-order/${orderId}`, reason);
  }
}
