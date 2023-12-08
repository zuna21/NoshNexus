import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IMenuItemRow } from 'src/app/_interfaces/IMenuItem';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-menu-item-gallery',
  standalone: true,
  imports: [
    CommonModule,
    GalleryModule,
    LightboxModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './menu-item-gallery.component.html',
  styleUrls: ['./menu-item-gallery.component.css']
})
export class MenuItemGalleryComponent implements OnInit {
  menuItem = this.data.menuItem;
  gallery: GalleryItem[] = [];
  isLoopFinished: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: {menuItem: IMenuItemRow}
  ) {}


  ngOnInit(): void {
    this.generateGallery();
  }

  generateGallery() {
    for (let image of this.menuItem.images) {
      const imageItem = new ImageItem({ src: 'http://localhost:5000/images/default/default.png', thumb: 'http://localhost:5000/images/default/default.png' });
      this.gallery.push(imageItem);
    }
    this.isLoopFinished = true;
  }

}
