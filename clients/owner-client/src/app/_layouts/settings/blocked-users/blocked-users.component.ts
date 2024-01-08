import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserCardComponent } from 'src/app/_components/user-card/user-card.component';
import { IUserCard } from 'src/app/_interfaces/IUser';
import { Subscription, mergeMap, of } from 'rxjs';
import { SettingService } from 'src/app/_services/setting.service';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { IBlockedCustomersParams } from 'src/app/_interfaces/query_params.interface';
import { BLOCKED_CUSTOMERS_PARAMS } from 'src/app/_default_values/default_query_params';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { FormsModule } from '@angular/forms';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-blocked-users',
  standalone: true,
  imports: [
    CommonModule,
    UserCardComponent,
    MatPaginatorModule,
    MatSelectModule,
    FormsModule,
    MatDialogModule,
  ],
  templateUrl: './blocked-users.component.html',
  styleUrls: ['./blocked-users.component.css'],
})
export class BlockedUsersComponent implements OnInit, OnDestroy {
  blockedCustomers: IUserCard[] = [];
  blockedCustomersQueryParams: IBlockedCustomersParams = {
    ...BLOCKED_CUSTOMERS_PARAMS,
  };
  totalItems: number = 0;
  restaurants: IRestaurantSelect[] = [{ id: -1, name: 'All Restaurants' }];
  selectedRestaurant: number = -1;

  blockedCustomerSub?: Subscription;
  restaurantSub?: Subscription;
  searchSub?: Subscription;
  unblockSub?: Subscription;

  constructor(
    private settingService: SettingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private restaurantService: RestaurantService,
    private searchBarService: SearchBarService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.setQueryParams();
    this.getRestaurants();
    this.getBlockedCustomers();
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
    const queryParams: Params = { ...this.blockedCustomersQueryParams };
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams,
    });
  }

  getBlockedCustomers() {
    this.blockedCustomerSub = this.activatedRoute.queryParams
      .pipe(
        mergeMap((_) =>
          this.settingService.getBlockedCustomers(
            this.blockedCustomersQueryParams
          )
        )
      )
      .subscribe({
        next: (response) => {
          this.blockedCustomers = [...response.result];
          this.totalItems = response.totalItems;
        },
      });
  }

  onPaginator(event: PageEvent) {
    this.blockedCustomersQueryParams = {
      ...this.blockedCustomersQueryParams,
      pageIndex: event.pageIndex,
    };

    this.setQueryParams();
  }

  onChangeRestaurant() {
    this.blockedCustomersQueryParams = {
      ...this.blockedCustomersQueryParams,
      pageIndex: 0,
      restaurant:
        this.selectedRestaurant === -1 ? null : this.selectedRestaurant,
    };

    this.setQueryParams();
  }

  onSearch() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: (search) => {
        this.blockedCustomersQueryParams = {
          ...this.blockedCustomersQueryParams,
          pageIndex: 0,
          search: search === '' ? null : search,
        };

        this.setQueryParams();
      },
    });
  }

  onUnblock(userId: number) {
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to unblock this user?`,
    };

    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    this.unblockSub = dialogRef
      .afterClosed()
      .pipe(
        mergeMap((answer) => {
          if (!answer) return of(null);
          return this.settingService.unblockCustomer(userId);
        })
      )
      .subscribe({
        next: (unblockCustomerId) => {
          this.blockedCustomers = this.blockedCustomers.filter(
            (x) => x.id !== unblockCustomerId
          );
        },
      });
  }

  ngOnDestroy(): void {
    this.blockedCustomerSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
    this.searchSub?.unsubscribe();
    this.unblockSub?.unsubscribe();
  }
}
