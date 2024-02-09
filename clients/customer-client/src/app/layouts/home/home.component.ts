import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { ITopNav, TopNavService } from '../../components/top-nav/top-nav.service';
import { IRestaurantCard, RestaurantCardComponent } from '../../components/restaurant-card/restaurant-card.component';
import { RestaurantService } from '../../services/restaurant.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RestaurantCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  restaurants = signal<IRestaurantCard[]>([]);

  restaurantSub?: Subscription;

  constructor(
    private topNavService: TopNavService,
    private restaurantService: RestaurantService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.setTopNav();
    this.getRestaurants();
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService.getRestaurants().subscribe({
      next: restaurants => this.restaurants.set(restaurants)
    });
  }

  setTopNav() {
    const topNav: ITopNav = {
      hasDrawerBtn: true,
      title: 'Restaurants'
    };
    this.topNavService.setTopNav(topNav);
  }

  onSelectedRestaurant(restaurantId: number) {
    this.router.navigateByUrl(`/restaurants/${restaurantId}`);
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
