import { Component, Input, signal } from '@angular/core';
import { IMenuItemCard } from '../../interfaces/menu-item.interface';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-menu-item-card',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './menu-item-card.component.html',
  styleUrl: './menu-item-card.component.css'
})
export class MenuItemCardComponent {
  @Input('menuItem') menuItem = signal<IMenuItemCard | undefined>(undefined);
}
