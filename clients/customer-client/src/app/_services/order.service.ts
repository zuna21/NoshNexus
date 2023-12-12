import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICreateOrder } from '../_interfaces/IOrder';
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
}
