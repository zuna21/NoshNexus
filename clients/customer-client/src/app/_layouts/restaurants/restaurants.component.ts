import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Subscription } from 'rxjs';
import { IRestaurantCard } from 'src/app/_interfaces/IRestaurant';
import { RestaurantCardComponent } from 'src/app/_components/restaurant-card/restaurant-card.component';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [
    CommonModule,
    RestaurantCardComponent,
    SearchBarComponent
  ],
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css']
})
export class RestaurantsComponent implements OnInit, OnDestroy {
  restaurants: IRestaurantCard[] = [];

  restaurantSub?: Subscription;

  constructor(
    private restaurantService: RestaurantService
  ) {}

  ngOnInit(): void {
    this.getRestaurants();
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService.getRestaurants().subscribe({
      next: restaurants => this.restaurants = restaurants
    });
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }

}
