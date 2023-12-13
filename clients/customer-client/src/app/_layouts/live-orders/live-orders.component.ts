import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ILiveRestaurantOrders } from 'src/app/_interfaces/IOrder';
import { Subscription } from 'rxjs';
import { OrderService } from 'src/app/_services/order.service';
import { ActivatedRoute } from '@angular/router';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';
import { OrderHubService } from 'src/app/_services/order-hub.service';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-live-orders',
  standalone: true,
  imports: [CommonModule, OrderCardComponent],
  templateUrl: './live-orders.component.html',
  styleUrls: ['./live-orders.component.css'],
})
export class LiveOrdersComponent implements OnInit, OnDestroy {
  restaurantOrders?: ILiveRestaurantOrders;
  restaurantId?: number;

  restaurantOrderSub?: Subscription;
  newOrderSub?: Subscription;

  constructor(
    private orderService: OrderService,
    private activatedRoute: ActivatedRoute,
    private orderHub: OrderHubService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getRestaurantOrders();
    this.connectToOrderHub();
    this.receiveNewOrder();
  }

  connectToOrderHub() {
    this.restaurantId = parseInt(
      this.activatedRoute.snapshot.params['restaurantId']
    );
    const token = this.accountService.getToken();
    if (!this.restaurantId || !token) return;
    this.orderHub.startConnection(token, this.restaurantId);
  }

  getRestaurantOrders() {
    this.restaurantId = this.activatedRoute.snapshot.params['restaurantId'];
    if (!this.restaurantId) return;
    this.restaurantOrderSub = this.orderService
      .getInProgressOrders(this.restaurantId)
      .subscribe({
        next: (restaurantOrders) => (this.restaurantOrders = restaurantOrders),
      });
  }

  receiveNewOrder() {
    this.newOrderSub = this.orderHub.newOrder$.subscribe({
      next: newOrder => {
        if (!this.restaurantOrders) return;
        this.restaurantOrders.orders = [...this.restaurantOrders.orders, newOrder];
      }
    });
  }

  ngOnDestroy(): void {
    this.restaurantOrderSub?.unsubscribe();
    this.newOrderSub?.unsubscribe();

    if (!this.restaurantId) return;
    this.orderHub.stopConnection(this.restaurantId);
  }
}
