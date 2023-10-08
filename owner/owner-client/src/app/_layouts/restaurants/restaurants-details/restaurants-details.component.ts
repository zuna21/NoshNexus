import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';
import { MatChipsModule } from '@angular/material/chips';
import { IRestaurantDetails } from 'src/app/_interfaces/IRestaurant';
import { RESTAURANT_FOR_DETAILS } from 'src/app/fake_data/restaurant';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-restaurants-details',
  standalone: true,
  imports: [CommonModule, GalleryModule, LightboxModule, MatDividerModule, MatButtonModule, MatIconModule, MatTooltipModule, MatDialogModule, MatChipsModule, RouterLink],
  templateUrl: './restaurants-details.component.html',
  styleUrls: ['./restaurants-details.component.css'],
})
export class RestaurantsDetailsComponent implements OnInit, OnDestroy {
  restaurant: IRestaurantDetails | undefined;
  dialogRefSub: Subscription | undefined;
  restaurantImages: ImageItem[] = [];

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurant = structuredClone(RESTAURANT_FOR_DETAILS);
    this.createRestaurantImageGallery(this.restaurant.restaurantImages);
  }

  createRestaurantImageGallery(imagesOfRestaurant: string[]) {
    for (let image of imagesOfRestaurant) {
      this.restaurantImages.push(new ImageItem({ src: image, thumb: image }));
    }
  }

  onDelete() {
    if (!this.restaurant) return;
    const question = `Are you sure you want to delete restaurant ${this.restaurant.name}?`
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: question
    });

    this.dialogRefSub = dialogRef.afterClosed().subscribe({
      next: answer => {
        if (!answer) return;
        // Izbrisi restoran
        console.log('Delete restaurant');
      }
    });
  }


  ngOnDestroy(): void {
    this.dialogRefSub?.unsubscribe();
  }
}
