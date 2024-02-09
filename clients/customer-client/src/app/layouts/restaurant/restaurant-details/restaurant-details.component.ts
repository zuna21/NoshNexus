import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { IRestaurant } from '../../../interfaces/restaurant.interface';
import { Subscription } from 'rxjs';
import { RestaurantService } from '../../../services/restaurant.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-restaurant-details',
  standalone: true,
  imports: [],
  templateUrl: './restaurant-details.component.html',
  styleUrl: './restaurant-details.component.css'
})
export class RestaurantDetailsComponent implements OnInit, OnDestroy {
  restaurant = signal<IRestaurant>({
    address: '',
    city: '',
    country: '',
    description: '',
    employeesNumber: -1,
    facebookUrl: '',
    id: -1,
    instagramUrl: '',
    isFavourite: true,
    isOpen: true,
    menusNumber: -1,
    name: '',
    phoneNumber: '',
    postalCode: -1,
    restaurantImages: [],
    websiteUrl: ''
  });
  restaurantId?: number;

  restaurantSub?: Subscription;

  constructor(
    private restaurantService: RestaurantService,
    private activatedRoute: ActivatedRoute
  ) {}
  
  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;
    this.restaurantSub = this.restaurantService.getRestaurant(this.restaurantId).subscribe({
      next: restaurant => {
        if (!restaurant) return;
        this.restaurant.set(restaurant);
      }
    });
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
