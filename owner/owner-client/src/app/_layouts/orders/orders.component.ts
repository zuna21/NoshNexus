import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription } from 'rxjs';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: 
  [
    CommonModule,
    OrderCardComponent
  ],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];

  orderSub: Subscription | undefined;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderSub = this.orderService.getOwnerOrders().subscribe({
      next: (orders) => (this.orders = orders),
    });
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
