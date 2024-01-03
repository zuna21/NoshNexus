import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription, mergeMap } from 'rxjs';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { OrderDeclineDialogComponent } from 'src/app/_components/order-card/order-decline-dialog/order-decline-dialog.component';
import { SharedCardsModule } from 'shared-cards';
import { IOrdersQueryParams } from 'src/app/_interfaces/query_params.interface';
import { ORDERS_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {MatSelectModule} from '@angular/material/select'; 
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    CommonModule, 
    MatDialogModule,
    SharedCardsModule,
    MatSelectModule,
    FormsModule
  ],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];
  ordersQueryParams: IOrdersQueryParams = {...ORDERS_QUERY_PARAMS};
  restaurants: IRestaurantSelect[] = [{id: -1, name: 'All Restaurants'}];
  restaurant: number = -1;

  orderSub?: Subscription;
  declineDialogSub?: Subscription;
  restaurantSub?: Subscription;

  constructor(
    private orderService: OrderService, 
    private dialog: MatDialog,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private restaurantService: RestaurantService
  ) {}

  ngOnInit(): void {
    this.getOrders();
    this.setQueryParams();
    this.getRestaurants();
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService.getOwnerRestaurantsForSelect().subscribe({
      next: restaurants => this.restaurants = [...this.restaurants, ...restaurants]
    });
  }

  setQueryParams() {
    const queryParams: Params = { ...this.ordersQueryParams };

    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams,
    });
  }

  getOrders() {
    this.orderSub = this.activatedRoute.queryParams.pipe(
      mergeMap(_ => this.orderService.getOwnerInProgressOrders(this.ordersQueryParams))
    ).subscribe({
      next: orders => this.orders = [...orders]
    });


    /* this.orderSub = this.orderService.getOwnerInProgressOrders().subscribe({
      next: (orders) => (this.orders = orders),
    }); */
  }

  onDecline(order: IOrderCard) {
    const dialogRef = this.dialog.open(OrderDeclineDialogComponent);
    this.declineDialogSub = dialogRef.afterClosed().subscribe({
      next: (declineReason) => {
        if (!declineReason) return;
        console.log(declineReason);
      },
    });
  }

  onChangeRestaurant() {
    this.ordersQueryParams = {
      ...this.ordersQueryParams,
      restaurant: this.restaurant === -1 ? null : this.restaurant
    };

    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.declineDialogSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
  }
}
