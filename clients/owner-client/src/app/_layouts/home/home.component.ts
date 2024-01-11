import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { ChartCardComponent } from 'src/app/_components/chart-card/chart-card.component';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { Subscription } from 'rxjs';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatSelectModule,
    ChartCardComponent,
    FormsModule,
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  restaurants: IRestaurantSelect[] = [];
  selectedRestaurant?: number;

  restaurantSub?: Subscription;

  constructor(
    private restaurantService: RestaurantService,
  ) {}

  ngOnInit(): void {
    this.getRestaurants();
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService.getOwnerRestaurantsForSelect().subscribe({
      next: restaurants => {
        this.restaurants = restaurants;
        if (this.restaurants.length > 0) this.selectedRestaurant = this.restaurants[0].id
      }
    });
  }


  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
