import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import {MatDividerModule} from '@angular/material/divider'; 
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-restaurants-details',
  standalone: true,
  imports: [CommonModule, GalleryModule, LightboxModule, MatDividerModule, MatButtonModule, MatIconModule],
  templateUrl: './restaurants-details.component.html',
  styleUrls: ['./restaurants-details.component.css'],
})
export class RestaurantsDetailsComponent {
  restaurantImages = [
    new ImageItem({
      src: 'assets/img/restaurants/restauran1.jpg',
      thumb: 'assets/img/restaurants/restauran1.jpg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran2.jpeg',
      thumb: 'assets/img/restaurants/restauran2.jpeg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran3.jpg',
      thumb: 'assets/img/restaurants/restauran3.jpg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran4.jpeg',
      thumb: 'assets/img/restaurants/restauran4.jpeg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran5.jpg',
      thumb: 'assets/img/restaurants/restauran5.jpg',
    }),
  ];
}
