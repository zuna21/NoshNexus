import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemCardComponent } from 'src/app/_components/menu-item-card/menu-item-card.component';

@Component({
  selector: 'app-menu-item-list',
  standalone: true,
  imports: [CommonModule, MenuItemCardComponent],
  templateUrl: './menu-item-list.component.html',
  styleUrls: ['./menu-item-list.component.css'],
})
export class MenuItemListComponent {}
