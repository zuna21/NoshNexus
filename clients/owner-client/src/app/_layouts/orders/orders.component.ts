import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription, merge, mergeMap, of } from 'rxjs';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { OrderDeclineDialogComponent } from 'src/app/_components/order-card/order-decline-dialog/order-decline-dialog.component';
import { SharedCardsModule } from 'shared-cards';
import { IOrdersQueryParams } from 'src/app/_interfaces/query_params.interface';
import { ORDERS_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { FormsModule } from '@angular/forms';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    SharedCardsModule,
    MatSelectModule,
    FormsModule,
    MatSlideToggleModule,
  ],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];
  ordersQueryParams: IOrdersQueryParams = { ...ORDERS_QUERY_PARAMS };
  restaurants: IRestaurantSelect[] = [{ id: -1, name: 'All Restaurants' }];
  restaurant: number = -1;
  canManage: boolean = false;

  orderSub?: Subscription;
  declineDialogSub?: Subscription;
  restaurantSub?: Subscription;
  searchSub?: Subscription;
  blockUserSub?: Subscription;
  acceptOrderSub?: Subscription;

  constructor(
    private orderService: OrderService,
    private dialog: MatDialog,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private restaurantService: RestaurantService,
    private searchBarService: SearchBarService
  ) {}

  ngOnInit(): void {
    this.getOrders();
    this.setQueryParams();
    this.getRestaurants();
    this.onSearch();
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService
      .getOwnerRestaurantsForSelect()
      .subscribe({
        next: (restaurants) =>
          (this.restaurants = [...this.restaurants, ...restaurants]),
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
    this.orderSub = this.activatedRoute.queryParams
      .pipe(
        mergeMap((_) =>
          this.orderService.getOwnerInProgressOrders(this.ordersQueryParams)
        )
      )
      .subscribe({
        next: (orders) => (this.orders = [...orders]),
      });

    /* this.orderSub = this.orderService.getOwnerInProgressOrders().subscribe({
      next: (orders) => (this.orders = orders),
    }); */
  }

  onAccept(orderCard: IOrderCard) {
    this.acceptOrderSub = this.orderService.accept(orderCard.id).subscribe({
      next: orderId => {
        if (!orderId) return;
        this.orders = this.orders.filter(x => x.id !== orderId);
      }
    });
  }

  onDecline(orderCard: IOrderCard) {
    const dialogRef = this.dialog.open(OrderDeclineDialogComponent);
    this.declineDialogSub = dialogRef.afterClosed().pipe(
      mergeMap((declineReason) => {
        if (!declineReason) return of(null);
        return this.orderService.decline(orderCard.id, declineReason);
      })
    ).subscribe({
      next: orderId => {
        this.orders = this.orders.filter(x => x.id !== orderId);
      }
    });
  }

  onChangeRestaurant() {
    this.ordersQueryParams = {
      ...this.ordersQueryParams,
      restaurant: this.restaurant === -1 ? null : this.restaurant,
    };

    this.setQueryParams();
  }

  onSearch() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: (search) => {
        this.ordersQueryParams = {
          ...this.ordersQueryParams,
          search: search === '' ? null : search,
        };

        this.setQueryParams();
      },
    });
  }

  onBlockUser(orderCard: IOrderCard) {
    const config: MatDialogConfig = {
      data: `Are you sure you want to block ${orderCard.user.username}?`,
    };
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, config);
    this.blockUserSub = dialogRef
      .afterClosed()
      .pipe(
        mergeMap((response) => {
          if (!response) return of(null);
          return this.orderService.blockCustomer(orderCard.id);
        })
      )
      .subscribe({
        next: (orderId) => {
          if (!orderId) return;
          this.orders = this.orders.filter((x) => x.id !== orderId);
        },
      });
  }


  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.declineDialogSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
    this.searchSub?.unsubscribe();
    this.blockUserSub?.unsubscribe();
    this.acceptOrderSub?.unsubscribe();
  }
}
