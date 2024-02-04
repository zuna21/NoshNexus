import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRestaurantDetails } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from '../../_services/restaurant.service';
import { Subscription } from 'rxjs';
import { GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-restaurant',
  standalone: true,
  imports: [
    CommonModule,
    GalleryModule,
    LightboxModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatDividerModule,
    MatChipsModule,
  ],
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnInit, OnDestroy {
  restaurant?: IRestaurantDetails;
  restaurantImages: ImageItem[] = [];
  isGalleryLoopFinished: boolean = false;

  restaurantSub?: Subscription;

  constructor(
    private restaurantService: RestaurantService
  ){}

  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurantSub = this.restaurantService.getRestaurant().subscribe({
      next: restaurant => {
        this.restaurant = restaurant;
        this.createRestaurantImageGallery(this.restaurant.restaurantImages);
      }
    });
  }

  createRestaurantImageGallery(imagesOfRestaurant: string[]) {
    for (let image of imagesOfRestaurant) {
      this.restaurantImages.push(new ImageItem({ src: image, thumb: image }));
    }
    this.isGalleryLoopFinished = true;
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
