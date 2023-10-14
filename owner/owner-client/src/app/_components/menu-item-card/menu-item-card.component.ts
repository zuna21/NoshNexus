import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { IMenuItemCard } from 'src/app/_interfaces/IMenu';

@Component({
  selector: 'app-menu-item-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule],
  templateUrl: './menu-item-card.component.html',
  styleUrls: ['./menu-item-card.component.css'],
})
export class MenuItemCardComponent {
  @Input('menuItem') menuItem: IMenuItemCard | undefined;
}
