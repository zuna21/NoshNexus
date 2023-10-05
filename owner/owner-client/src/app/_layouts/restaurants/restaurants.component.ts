import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantCardComponent } from 'src/app/_components/restaurant-card/restaurant-card.component';
import { IRestaurantCard } from 'src/app/_interfaces/IRestaurant';
import { RESTAURANTS_FOR_CARD } from 'src/app/fake_data/restaurant';

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [CommonModule, RestaurantCardComponent],
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css']
})
export class RestaurantsComponent implements OnInit {
  restaurants: IRestaurantCard[] = [];

  ngOnInit(): void {
    this.loadRestaurants();
  }

  loadRestaurants() {
    this.restaurants = structuredClone(RESTAURANTS_FOR_CARD);
  }
}
