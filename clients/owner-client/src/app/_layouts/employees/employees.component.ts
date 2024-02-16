import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeCardComponent } from 'src/app/_components/employee-card/employee-card.component';
import { IEmployeeCard } from 'src/app/_interfaces/IEmployee';
import { Subscription, mergeMap } from 'rxjs';
import { EmployeeService } from 'src/app/_services/employee.service';
import { IEmployeesQueryParams } from 'src/app/_interfaces/query_params.interface';
import { EMPLOYEES_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import {MatSelectModule} from '@angular/material/select'; 
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { RestaurantStore } from 'src/app/_store/restaurant.store';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    CommonModule, 
    EmployeeCardComponent,
    MatPaginatorModule,
    MatSelectModule,
    TranslateModule
  ],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
})
export class EmployeesComponent implements OnInit, OnDestroy {
  employeesCards: IEmployeeCard[] = [];
  employeesQueryParams: IEmployeesQueryParams = {...EMPLOYEES_QUERY_PARAMS}
  totalItems: number = 0;
  restaurants: IRestaurantSelect[] = [{id: -1, name: 'all restaurants'}];

  employeesCardSub?: Subscription;
  searchSub?: Subscription;
  restaurantSub?: Subscription;

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private searchBarService: SearchBarService,
    private restaurantService: RestaurantService,
    private restaurantStore: RestaurantStore
  ) {}

  ngOnInit(): void {
    this.setQueryParams();
    this.getEmployeesCards();
    this.getRestaurants();
    this.onSearch();
  }

  setQueryParams() {
    const queryParams: Params = {...this.employeesQueryParams};
    this.router.navigate(
      [], 
      {
        relativeTo: this.activatedRoute,
        queryParams
      }
    );
  }

  getRestaurants() {
    const restaurantsInStore = this.restaurantStore.getRestaurantsForSelect();
    if (restaurantsInStore.length <= 0) {
      this.restaurantSub = this.restaurantService.getOwnerRestaurantsForSelect().subscribe({
        next: restaurants => {
          this.restaurantStore.setRestaurantsForSelect(restaurants);
          this.restaurants = [...this.restaurants, ...restaurants];
        }
      });
    } else {
      this.restaurants = [...this.restaurants, ...restaurantsInStore];
    }
  }

  getEmployeesCards() {
    this.employeesCardSub = this.activatedRoute.queryParams.pipe(
      mergeMap(_ => this.employeeService.getEmployees(this.employeesQueryParams))
    ).subscribe({
      next: result => {
        this.employeesCards = [...result.result];
        this.totalItems = result.totalItems;
      }
    });
  }

  onSearch() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: search => {
        this.employeesQueryParams = {
          ...this.employeesQueryParams,
          pageIndex: 0,
          search: search === '' ? null : search
        }

        this.setQueryParams();
      }
    });
  }

  onPaginator(event: PageEvent) {
    this.employeesQueryParams = {
      ...this.employeesQueryParams,
      pageIndex: event.pageIndex
    };
    this.setQueryParams();
  }

  onChangeRestaurant(restaurantId: number) {
    this.employeesQueryParams = {
      ...this.employeesQueryParams,
      pageIndex: 0,
      restaurant: restaurantId === -1 ? null : restaurantId
    };

    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.employeesCardSub?.unsubscribe();
    this.searchSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
  }
}
