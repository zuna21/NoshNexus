import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IOrder } from '../_interfaces/IOrder';
import { IMenuItemRow } from '../_interfaces/IMenuItem';

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

    const newOrder = {
      ...order,
      totalItems: totalItems,
      totalPrice: this.calculateTotalPrice(order.menuItems)
    };
    if (this.allBelongsToSameRestaurant(newOrder.menuItems)) this.order.next(newOrder);
    else this.order.next(this.initOrder);
    console.log(newOrder);
  }

  getOrder(): IOrder {
    return this.order.getValue();
  }



  // Private functions
  private allBelongsToSameRestaurant(menuItems: IMenuItemRow[]): boolean {
    if (menuItems.length <= 0) return false;
    const restId = menuItems[0].restaurantId;
    for (let item of menuItems) {
      if (item.restaurantId !== restId) return false;
    }

    return true;
  }

  private calculateTotalPrice(menuItems: IMenuItemRow[]): number {
    if (menuItems.length <= 0) return 0;
    let sum = 0;
    for (let item of menuItems) {
      if (item.hasSpecialOffer) sum += item.specialOfferPrice;
      else sum += item.price;
    }

    return sum;
  }
}
