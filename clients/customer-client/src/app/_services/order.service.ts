import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICreateOrder, ILiveRestaurantOrders, IOrderCard } from '../_interfaces/IOrder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(
    private http: HttpClient
  ) { }

  makeOrder(restaurantId: number, order: ICreateOrder): Observable<boolean> {
    return this.http.post<boolean>(`http://localhost:5000/api/orders/create/${restaurantId}`, order);
  }

  getInProgressOrders(restaurantId: number): Observable<ILiveRestaurantOrders> {
    return this.http.get<ILiveRestaurantOrders>(`http://localhost:5000/api/orders/get-in-progress-orders/${restaurantId}`);
  }

  getOrders(sq: string = ''): Observable<IOrderCard[]> {
    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/orders/get-orders?sq=${sq}`);
  }

  getDeclinedOrders(sq: string = ''): Observable<IOrderCard[]> {
    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/orders/get-declined-orders?sq=${sq}`);
  }

  getAcceptedOrders(sq: string = ''): Observable<IOrderCard[]> {
    return this.http.get<IOrderCard[]>(`http://localhost:5000/api/orders/get-accepted-orders?sq=${sq}`);
  }
}
