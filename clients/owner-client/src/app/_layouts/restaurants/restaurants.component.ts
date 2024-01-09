import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRestaurantCard } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Subscription, mergeMap } from 'rxjs';
import { SharedCardsModule } from 'shared-cards';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { IRestaurantsQueryParams } from 'src/app/_interfaces/query_params.interface';
import { RESTAURANTS_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';

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
  restaurantsQueryParams: IRestaurantsQueryParams = {...RESTAURANTS_QUERY_PARAMS};

  restaurantSub?: Subscription;
  searchSub?: Subscription;

  constructor(
    private restaurantService: RestaurantService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private searchBarService: SearchBarService,
  ) { }

  ngOnInit(): void {
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

  loadRestaurants() {
    this.restaurantSub = this.activatedRoute.queryParams.pipe(
      mergeMap(_ => this.restaurantService.getRestaurants(this.restaurantsQueryParams))
    ).subscribe({
      next: result => {
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
