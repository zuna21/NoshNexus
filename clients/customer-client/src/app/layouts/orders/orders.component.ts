import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { IOrderCard } from '../../interfaces/order.interface';
import { Subscription } from 'rxjs';
import { OrderService } from '../../services/order.service';
import { OrderCardComponent } from '../../components/order-card/order-card.component';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    OrderCardComponent
  ],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders = signal<IOrderCard[]>([]);

  orderSub?: Subscription;

  constructor(
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderSub = this.orderService.getOrders().subscribe({
      next: orders => this.orders.set(orders)
    });
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
