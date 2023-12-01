import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Subscription } from 'rxjs';
import { MatChipsModule } from '@angular/material/chips';
import { IRestaurantDetails } from 'src/app/_interfaces/IRestaurant';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RestaurantService } from 'src/app/_services/restaurant.service';

@Component({
  selector: 'app-restaurants-details',
  standalone: true,
  imports: [
    CommonModule,
    GalleryModule,
    LightboxModule,
    MatDividerModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule,
    RouterLink,
  ],
  templateUrl: './restaurants-details.component.html',
  styleUrls: ['./restaurants-details.component.css'],
})
export class RestaurantsDetailsComponent implements OnInit, OnDestroy {
  restaurant: IRestaurantDetails | undefined;
  restaurantImages: ImageItem[] = [];
  restaurantId: string = '';
  isGalleryLoopFinished: boolean = false;

  dialogRefSub: Subscription | undefined;
  restaurantSub: Subscription | undefined;

  constructor(
    private restaurantService: RestaurantService,
    private activatedRoute: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurantId = this.activatedRoute.snapshot.params['id'];
    if (!this.restaurantId) return;
    this.restaurantSub = this.restaurantService
      .getRestaurant(this.restaurantId)
      .subscribe({
        next: (restaurant) => {
          this.restaurant = restaurant;
          this.createRestaurantImageGallery(this.restaurant.restaurantImages);
        },
      });
  }

  createRestaurantImageGallery(imagesOfRestaurant: string[]) {
    for (let image of imagesOfRestaurant) {
      this.restaurantImages.push(new ImageItem({ src: image, thumb: image }));
    }
    this.isGalleryLoopFinished = true;
  }

  ngOnDestroy(): void {
    this.dialogRefSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
  }
}
