import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IOrderCard } from './order-card.interface';

@Component({
  selector: 'lib-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css']
})
export class OrderCardComponent {
  @Input('order') order: IOrderCard | undefined;
  @Input('hasBtns') hasBtns: boolean = true;

  @Output('accept') accept = new EventEmitter<IOrderCard>();
  @Output('decline') decline = new EventEmitter<IOrderCard>();
  @Output('block') block = new EventEmitter<IOrderCard>();
  @Output('restaurant') restaurant = new EventEmitter<number>(); 
  @Output('menuItem') menuItem = new EventEmitter<number>();

  
  onAccept() {
    if (!this.order) return;
    this.accept.emit(this.order);
  }

  onDecline() {
    if (!this.order) return;
    this.decline.emit(this.order);
  }

  onBlock() {
    if (!this.order) return;
    this.block.emit(this.order);
  }

  onRestaurant() {
    if (!this.order) return;
    this.restaurant.emit(this.order.restaurant.id);
  }

  onMenuItem(menuItemId: number) {
    if (!this.order) return;
    this.menuItem.emit(menuItemId);
  }
}
