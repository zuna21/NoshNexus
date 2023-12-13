import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ILiveRestaurantOrders } from 'src/app/_interfaces/IOrder';
import { Subscription } from 'rxjs';
import { OrderService } from 'src/app/_services/order.service';
import { ActivatedRoute } from '@angular/router';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';

@Component({
  selector: 'app-live-orders',
  standalone: true,
  imports: [
    CommonModule,
    OrderCardComponent
  ],
  templateUrl: './live-orders.component.html',
  styleUrls: ['./live-orders.component.css']
})
export class LiveOrdersComponent implements OnInit, OnDestroy {
  restaurantOrders?: ILiveRestaurantOrders;
  restaurantId?: number;

  restaurantOrderSub?: Subscription;

  constructor(
    private orderService: OrderService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getRestaurantOrders();
  }

  getRestaurantOrders() {
    this.restaurantId = this.activatedRoute.snapshot.params['restaurantId'];
    if (!this.restaurantId) return;
    this.restaurantOrderSub = this.orderService.getInProgressOrders(this.restaurantId)
      .subscribe({
        next: restaurantOrders => this.restaurantOrders = restaurantOrders
      });
  }

  ngOnDestroy(): void {
    this.restaurantOrderSub?.unsubscribe();
  }
}
