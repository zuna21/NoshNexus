import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IOrderCard } from '../_interfaces/IOrder';
import { IOrdersHistoryQueryParams, IOrdersQueryParams } from '../_interfaces/query_params.interface';
import { IPagedList } from '../_interfaces/IPagedList';
import { environment } from 'src/environments/environment';

const OWNER_URL: string = `${environment.apiUrl}/owner/orders`;
const EMPLOYEE_URL: string = `${environment.apiUrl}/employee/orders`;

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}

  getOwnerInProgressOrders(ordersQueryParams: IOrdersQueryParams): Observable<IOrderCard[]> {
    let params = new HttpParams();

    if (ordersQueryParams.restaurant) params = params.set('restaurant', ordersQueryParams.restaurant);
    if (ordersQueryParams.search) params = params.set('search', ordersQueryParams.search);

    return this.http.get<IOrderCard[]>(`${OWNER_URL}/get-in-progress-orders`, { params });
  }

  getOrdersHistory(ordersHistoryQueryParams: IOrdersHistoryQueryParams): Observable<IPagedList<IOrderCard[]>> {
    let params = new HttpParams();

    params = params.set('pageIndex', ordersHistoryQueryParams.pageIndex);
    params = params.set('status', ordersHistoryQueryParams.status);

    if (ordersHistoryQueryParams.restaurant) params = params.set('restaurant', ordersHistoryQueryParams.restaurant);
    if (ordersHistoryQueryParams.search) params = params.set('search', ordersHistoryQueryParams.search);

    return this.http.get<IPagedList<IOrderCard[]>>(`${OWNER_URL}/get-orders-history`, { params });
  }

  blockCustomer(orderId: number): Observable<number> {
    return this.http.get<number>(`${OWNER_URL}/block-customer/${orderId}`);
  }

  accept(orderId: number): Observable<number> {
    return this.http.get<number>(`${OWNER_URL}/accept-order/${orderId}`);
  }

  decline(orderId: number, reason: string): Observable<number> {
    return this.http.put<number>(`${OWNER_URL}/decline-order/${orderId}`, reason);
  }

  getEmployeeInProgressOrders(): Observable<IOrderCard[]> {
    return this.http.get<IOrderCard[]>(`${EMPLOYEE_URL}/get-in-progress-orders`);
  }
}
