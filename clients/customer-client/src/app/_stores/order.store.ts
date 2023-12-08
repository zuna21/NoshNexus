import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IOrder } from '../_interfaces/IOrder';

@Injectable({
  providedIn: 'root',
})
export class OrderStore {
  initOrder: IOrder = {
    menuItems: [],
    totalItems: 0,
    totalPrice: 0
  };
  private order = new BehaviorSubject<IOrder>(this.initOrder);
  order$ = this.order.asObservable();

  setOrder(order: IOrder) {
    const totalItems = order.menuItems.length;
    let totalPrice = 0;
    for (let item of order.menuItems) {
      if (item.hasSpecialOffer) totalPrice += item.specialOfferPrice;
      else totalPrice += item.price;
    }

    const newOrder = {
      ...order,
      totalItems: totalItems,
      totalPrice: totalPrice
    };
    this.order.next(newOrder);
    console.log(newOrder);
  }

  getOrder(): IOrder {
    return this.order.getValue();
  }
}
