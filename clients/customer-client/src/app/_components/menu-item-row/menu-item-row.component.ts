import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatExpansionModule} from '@angular/material/expansion'; 
import { MatButtonModule } from '@angular/material/button';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import { MatIconModule } from '@angular/material/icon';
import { IMenuItemRow } from 'src/app/_interfaces/IMenuItem';

@Component({
  selector: 'app-menu-item-row',
  standalone: true,
  imports: [
    CommonModule,
    MatExpansionModule,
    MatButtonModule,
    GalleryModule,
    LightboxModule,
    MatProgressSpinnerModule,
    MatIconModule
  ],
  templateUrl: './menu-item-row.component.html',
  styleUrls: ['./menu-item-row.component.css']
})
export class MenuItemRowComponent implements OnInit {
  @Input('menuItem') menuItem?: IMenuItemRow

  imageGallery: GalleryItem[] = [];
  isImageLoading: boolean = true;

  ngOnInit(): void {
    this.generateImageGallery();
  }

  generateImageGallery() {
    if (!this.menuItem) return;
    for (let image of this.menuItem.images) {
      const imageItem = new ImageItem({ src: 
        'https://upload.wikimedia.org/wikipedia/commons/9/91/Pizza-3007395.jpg', thumb: 
      'https://upload.wikimedia.org/wikipedia/commons/9/91/Pizza-3007395.jpg' });
      this.imageGallery.push(imageItem);
    }
  }
}
