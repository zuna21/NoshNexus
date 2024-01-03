import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription } from 'rxjs';
import { SharedCardsModule } from 'shared-cards';
import {MatSelectModule} from '@angular/material/select'; 
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ORDERS_HISTORY_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';

@Component({
  selector: 'app-orders-history',
  standalone: true,
  imports: [
    CommonModule,
    SharedCardsModule,
    MatSelectModule,
    FormsModule
  ],
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.css'],
})
export class OrdersHistoryComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];
  restaurants: IRestaurantSelect[] = [{id: -1, name: "All Restaurants"}];
  restaurant: number = -1;
  ordersHistoryQueryParams = {...ORDERS_HISTORY_QUERY_PARAMS};

  orderSub: Subscription | undefined;
  restaurantSub?: Subscription;

  constructor(
    private orderService: OrderService,
    private restaurantService: RestaurantService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getOrders();
    this.setQueryParams();
    this.getRestaurants();
  }

  setQueryParams() {
    const queryParams: Params = {...this.ordersHistoryQueryParams};

    this.router.navigate(
      [], 
      {
        relativeTo: this.activatedRoute,
        queryParams,
      }
    );
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService.getOwnerRestaurantsForSelect().subscribe({
      next: restaurants => this.restaurants = [...this.restaurants, ...restaurants]
    });
  }

  getOrders() {
    this.orderSub = this.orderService.getOrdersHistory().subscribe({
      next: (orders) => (this.orders = orders),
    });
  }

  onChangeRestaurant() {
    this.ordersHistoryQueryParams = {
      ...this.ordersHistoryQueryParams,
      restaurant: this.restaurant === -1 ? null : this.restaurant
    };
    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
  }
}
