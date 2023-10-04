import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { GalleryItem, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-restaurants-create',
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css']
})
export class RestaurantsCreateComponent implements OnInit {

  restaurantImages: GalleryItem[] = [];

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon('facebook-logo', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/facebook-logo.svg'));
    iconRegistry.addSvgIcon('instagram-logo', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/instagram-logo.svg'));
    iconRegistry.addSvgIcon('globe', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/globe.svg'));
    iconRegistry.addSvgIcon('image', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/image.svg'));
    iconRegistry.addSvgIcon('images', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/images.svg'));
    iconRegistry.addSvgIcon('save', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/save.svg'));
  }

  ngOnInit(): void {
      this.loadRestaurantImages();
  }

  loadRestaurantImages() {
    this.restaurantImages = [
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
    ];
  }

}
