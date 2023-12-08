import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatExpansionModule} from '@angular/material/expansion'; 
import { MatButtonModule } from '@angular/material/button';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { IMenuItemRow } from 'src/app/_interfaces/IMenuItem';
import {MatDialog, MatDialogConfig, MatDialogModule} from '@angular/material/dialog'; 
import { MenuItemGalleryComponent } from './menu-item-gallery/menu-item-gallery.component';

@Component({
  selector: 'app-menu-item-row',
  standalone: true,
  imports: [
    CommonModule,
    MatExpansionModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatDialogModule,
  ],
  templateUrl: './menu-item-row.component.html',
  styleUrls: ['./menu-item-row.component.css']
})
export class MenuItemRowComponent {
  @Input('menuItem') menuItem?: IMenuItemRow

  isImageLoading: boolean = true;

  constructor(
    private dialog: MatDialog
  ) {}

  onImage(event: MouseEvent) {
    event.stopPropagation();
    if (!this.menuItem) return;
    const dialogConfig: MatDialogConfig = {
      data: {
        menuItem: this.menuItem
      }
    }
    this.dialog.open(MenuItemGalleryComponent, dialogConfig);
  }
}
