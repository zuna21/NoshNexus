import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantCardComponent } from 'src/app/_components/restaurant-card/restaurant-card.component';
import { IRestaurantCard } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [CommonModule, RestaurantCardComponent],
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
    this.restaurantSub = this.restaurantService.getOwnerRestaurants().subscribe({
      next: restaurants => this.restaurants = restaurants
    });
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
