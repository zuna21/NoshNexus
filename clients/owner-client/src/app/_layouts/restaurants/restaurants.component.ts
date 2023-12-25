import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRestaurantCard } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Subscription } from 'rxjs';
import { SharedCardsModule } from 'shared-cards';

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [
    CommonModule, 
    SharedCardsModule
  ],
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css']
})
export class RestaurantsComponent implements OnInit, OnDestroy {
  restaurants: IRestaurantCard[] = [];

  restaurantSub: Subscription | undefined;

  constructor(
    private restaurantService: RestaurantService
  ) { }

  ngOnInit(): void {
    this.loadRestaurants();
  }

  loadRestaurants() {
    this.restaurantSub = this.restaurantService.getRestaurants().subscribe({
      next: restaurants => this.restaurants = restaurants
    });
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
