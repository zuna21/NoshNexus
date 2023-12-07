import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRestaurantDetails } from 'src/app/_interfaces/IRestaurant';
import { Subscription } from 'rxjs';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import {MatDividerModule} from '@angular/material/divider'; 
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'app-restaurant-details',
  standalone: true,
  imports: [
    CommonModule,
    GalleryModule,
    LightboxModule,
    MatDividerModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './restaurant-details.component.html',
  styleUrls: ['./restaurant-details.component.css']
})
export class RestaurantDetailsComponent implements OnInit, OnDestroy {
  restaurant?: IRestaurantDetails;
  restaurantId?: number;
  imageGallery: GalleryItem[] = [];

  restaurantSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private restaurantService: RestaurantService
  ) {}

  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurantId = this.activatedRoute.snapshot.params['id'];
    if (!this.restaurantId) return;
    this.restaurantSub = this.restaurantService.getRestaurant(this.restaurantId).subscribe({
      next: restaurant => {
        if (!restaurant) return;
        this.restaurant = restaurant;
        this.generateImageGallery(this.restaurant.restaurantImages);
        console.log(this.restaurant);
      }
    });
  }

  generateImageGallery(images: string[]) {
    for (let image of images) {
      const imageObj = new ImageItem({ src: image, thumb: image });
      this.imageGallery.push(imageObj);
    };
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
