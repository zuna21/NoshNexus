import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatExpansionModule} from '@angular/material/expansion'; 
import { MatButtonModule } from '@angular/material/button';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { GalleryModule } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import { MatIconModule } from '@angular/material/icon';

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
export class MenuItemRowComponent {
  isImageLoading: boolean = true;
}
