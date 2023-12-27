import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { ChartCardComponent } from 'src/app/_components/chart-card/chart-card.component';
import { ALL_CHARTS } from 'src/app/_charts/all-charts';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { Subscription } from 'rxjs';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatSelectModule,
    ChartCardComponent,
    FormsModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  charts = ALL_CHARTS;
  restaurants: IRestaurantSelect[] = [];
  selectedRestaurant?: number;

  restaurantSub?: Subscription;

  constructor(
    private restaurantService: RestaurantService,
    private router: Router
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

  onNavigate(chartId: number) {
    if (!this.selectedRestaurant) return;
    switch (chartId) {
      case 1:
        this.router.navigateByUrl(`/charts/week-day-orders/${this.selectedRestaurant}`);
        break;
      case 2: 
        this.router.navigateByUrl(`/charts/top-ten-menu-items/${this.selectedRestaurant}`);
        break;
      case 3:
        this.router.navigateByUrl(`/charts/week-orders-by-hour/${this.selectedRestaurant}`);
        break;
      default:
        break;
    }
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
