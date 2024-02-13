import { Injectable } from '@angular/core';
import { IOrder } from '../interfaces/order.interface';
import { BehaviorSubject } from 'rxjs';
import { IMenuItemCard } from '../interfaces/menu-item.interface';

const DEFAULT_ORDER: IOrder = {
  menuItems: [],
  note: '',
  tableId: -1,
  totalMenuItems: 0,
  totalPrice: 0
};

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private order = new BehaviorSubject<IOrder>(DEFAULT_ORDER);
  order$ = this.order.asObservable();

  selectTable(tableId: number) {
    const updatedOrder: IOrder = {
      ...this.order.getValue(),
      tableId: tableId
    };
    this.order.next(updatedOrder);
  }

  addNote(note: string) {
    const updatedOrder: IOrder = {
      ...this.order.getValue(),
      note: note
    };
    this.order.next(updatedOrder);
  }

  addMenuItem(menuItem: IMenuItemCard) {
    const updatedMenuItems = [...this.order.getValue().menuItems, menuItem];
    const totalMenuItems = updatedMenuItems.length;
    const totalPrice = this.calculateTotalPrice(updatedMenuItems);
    const updatedOrder: IOrder = {
      ...this.order.getValue(),
      menuItems: updatedMenuItems,
      totalMenuItems: totalMenuItems,
      totalPrice: totalPrice
    };
    this.order.next(updatedOrder);
  }

  removeMenuItem(menuItemId: number) {
    const removedMenuItemIndex = [...this.order.getValue().menuItems].findIndex(x => x.id === menuItemId);
    if (removedMenuItemIndex < 0) return;
    const updatedMenuItems = [...this.order.getValue().menuItems];
    updatedMenuItems.splice(removedMenuItemIndex, 1);
    const totalMenuItems = updatedMenuItems.length;
    const totalPrice = this.calculateTotalPrice(updatedMenuItems);
    const updatedOrder: IOrder = {
      ...this.order.getValue(),
      menuItems: updatedMenuItems,
      totalMenuItems: totalMenuItems,
      totalPrice: totalPrice
    };
    this.order.next(updatedOrder);
  }

  resetOrder() {
    this.order.next(DEFAULT_ORDER);
  }

  calculateTotalPrice(menuItems: IMenuItemCard[]): number {
    let totalPrice = 0;
    for (let menuItem of menuItems) {
      if (menuItem.hasSpecialOffer) totalPrice += menuItem.specialOfferPrice;
      else totalPrice += menuItem.price;
    };
    return totalPrice;
  }

}
