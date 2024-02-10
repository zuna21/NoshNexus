import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { IRestaurant } from '../../../interfaces/restaurant.interface';
import { Subscription } from 'rxjs';
import { RestaurantService } from '../../../services/restaurant.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NgStyle } from '@angular/common';
import {MatDividerModule} from '@angular/material/divider';


@Component({
  selector: 'app-restaurant-details',
  standalone: true,
  imports: [
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    NgStyle,
    MatDividerModule,
    RouterLink
  ],
  templateUrl: './restaurant-details.component.html',
  styleUrl: './restaurant-details.component.css'
})
export class RestaurantDetailsComponent implements OnInit, OnDestroy {
  restaurant = signal<IRestaurant | undefined>(undefined);
  restaurantId?: number;
  isImageLoading = signal<boolean>(true);

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
        console.log(this.restaurant());
      }
    });
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
