import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemDialogPreviewComponent } from './menu-item-dialog-preview/menu-item-dialog-preview.component';

@Component({
  selector: 'app-menu-item-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MenuItemDialogPreviewComponent
  ],
  templateUrl: './menu-item-dialog.component.html',
  styleUrls: ['./menu-item-dialog.component.css']
})
export class MenuItemDialogComponent {

}
