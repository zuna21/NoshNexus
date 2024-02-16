import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription, mergeMap } from 'rxjs';
import { MatSelectModule } from '@angular/material/select';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ORDERS_HISTORY_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { RestaurantStore } from 'src/app/_store/restaurant.store';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-orders-history',
  standalone: true,
  imports: [
    CommonModule,
    MatSelectModule,
    FormsModule,
    MatButtonToggleModule,
    MatPaginatorModule,
    OrderCardComponent,
    TranslateModule
  ],
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.css'],
})
export class OrdersHistoryComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];
  restaurants: IRestaurantSelect[] = [{ id: -1, name: 'all restaurants' }];
  restaurant: number = -1;
  ordersHistoryQueryParams = { ...ORDERS_HISTORY_QUERY_PARAMS };
  status: 'all' | 'declined' | 'accepted' = 'all';
  totalItems: number = 0;

  orderSub?: Subscription;
  restaurantSub?: Subscription;
  searchSub?: Subscription;

  constructor(
    private orderService: OrderService,
    private restaurantService: RestaurantService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private searchBarService: SearchBarService,
    private restaurantStore: RestaurantStore
  ) {}

  ngOnInit(): void {
    this.getOrders();
    this.setQueryParams();
    this.getRestaurants();
    this.onSearch();
  }

  setQueryParams() {
    const queryParams: Params = { ...this.ordersHistoryQueryParams };
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams,
    });
  }

  getRestaurants() {
    const restaurantsFromStore = this.restaurantStore.getRestaurantsForSelect();
    if (restaurantsFromStore.length <= 0) {
      this.restaurantSub = this.restaurantService
        .getOwnerRestaurantsForSelect()
        .subscribe({
          next: (restaurants) => {
            this.restaurantStore.setRestaurantsForSelect(restaurants);
            this.restaurants = [...this.restaurants, ...restaurants];
          },
        });
    } else {
      this.restaurants = [...this.restaurants, ...restaurantsFromStore];
    }
  }

  getOrders() {
    this.orderSub = this.activatedRoute.queryParams
      .pipe(
        mergeMap((_) =>
          this.orderService.getOrdersHistory(this.ordersHistoryQueryParams)
        )
      )
      .subscribe({
        next: (response) => {
          this.orders = [...response.result];
          this.totalItems = response.totalItems;
        },
      });
  }

  onChangeRestaurant() {
    this.ordersHistoryQueryParams = {
      ...this.ordersHistoryQueryParams,
      pageIndex: 0,
      restaurant: this.restaurant === -1 ? null : this.restaurant,
    };
    this.setQueryParams();
  }

  onSearch() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: (search) => {
        this.ordersHistoryQueryParams = {
          ...this.ordersHistoryQueryParams,
          pageIndex: 0,
          search: search === '' ? null : search,
        };

        this.setQueryParams();
      },
    });
  }

  onChangeStatus() {
    this.ordersHistoryQueryParams = {
      ...this.ordersHistoryQueryParams,
      pageIndex: 0,
      status: this.status,
    };
    this.setQueryParams();
  }

  onPaginator(event: PageEvent) {
    this.ordersHistoryQueryParams = {
      ...this.ordersHistoryQueryParams,
      pageIndex: event.pageIndex,
    };

    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
    this.searchSub?.unsubscribe();
  }
}
