import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRestaurantCard } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Subscription, mergeMap, of } from 'rxjs';
import { SharedCardsModule } from 'shared-cards';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { IRestaurantsQueryParams } from 'src/app/_interfaces/query_params.interface';
import { RESTAURANTS_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import { RestaurantStore } from 'src/app/_stores/restaurant.store';

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [
    CommonModule, 
    SharedCardsModule,
  ],
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css']
})
export class RestaurantsComponent implements OnInit, OnDestroy {
  restaurants: IRestaurantCard[] = [];
  restaurantsQueryParams: IRestaurantsQueryParams | null = null;
  
  restaurantSub?: Subscription;
  searchSub?: Subscription;
  initRestaurantSub?: Subscription;

  constructor(
    private restaurantService: RestaurantService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private searchBarService: SearchBarService,
    public restaurantStore: RestaurantStore
  ) { }

  ngOnInit(): void {
    this.loadInitRestaurants();
    this.loadRestaurants();
    this.setQueryParams();
    this.onSearch();
  }

  setQueryParams() {
    const queryParams: Params = {...this.restaurantsQueryParams};

    this.router.navigate(
      [], 
      {
        relativeTo: this.activatedRoute,
        queryParams
      }
    );
  }

  onSearch() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: search => {
        this.restaurantsQueryParams = {
          ...this.restaurantsQueryParams,
          search: search ? search : null
        };

        this.setQueryParams();
      }
    });
  }

  loadInitRestaurants() {
    if (!this.restaurantsQueryParams && this.restaurantStore.getRestaurants().length <= 0) {
      this.initRestaurantSub = this.restaurantService.getRestaurants().subscribe({
        next: restaurants => {
          this.restaurantStore.setRestaurants(restaurants);
          this.restaurants = [...restaurants];
        }
      });
    }

    this.restaurants = [...this.restaurantStore.getRestaurants()];
  }

  loadRestaurants() {
    this.restaurantSub = this.activatedRoute.queryParams.pipe(
      mergeMap((_ => {
        if (!this.restaurantsQueryParams) return of(null);
        return this.restaurantService.getRestaurants(this.restaurantsQueryParams)
      }))
    ).subscribe({
      next: result => {
        if (!result) return;
        this.restaurants = [...result];
      }
    });
  }

  onSeeMore(restaurantId: number) {
    this.router.navigateByUrl(`/restaurants/${restaurantId}`);
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
    this.searchSub?.unsubscribe();
  }
}
