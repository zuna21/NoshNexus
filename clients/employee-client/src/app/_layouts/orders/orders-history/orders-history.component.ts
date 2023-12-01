import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription } from 'rxjs';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';

@Component({
  selector: 'app-orders-history',
  standalone: true,
  imports: [
    CommonModule,
    OrderCardComponent
  ],
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.css'],
})
export class OrdersHistoryComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];

  orderSub: Subscription | undefined;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderSub = this.orderService.getOwnerOrdersHistory().subscribe({
      next: (orders) => (this.orders = orders),
    });
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
