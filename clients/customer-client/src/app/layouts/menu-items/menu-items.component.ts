import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItemService } from '../../services/menu-item.service';
import { IMenuItemCard } from '../../interfaces/menu-item.interface';
import { Subscription } from 'rxjs';
import { MenuItemCardComponent } from '../../components/menu-item-card/menu-item-card.component';

@Component({
  selector: 'app-menu-items',
  standalone: true,
  imports: [
    MenuItemCardComponent
  ],
  templateUrl: './menu-items.component.html',
  styleUrl: './menu-items.component.css'
})
export class MenuItemsComponent implements OnInit, OnDestroy {
  restaurantId?: number;
  menuItems = signal<IMenuItemCard[]>([]);

  menuItemSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private menuItemService: MenuItemService
  ) {}

  ngOnInit(): void {
    this.getMenuItems();
  }

  getMenuItems() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;
    console.log(this.restaurantId);
    this.menuItemSub = this.menuItemService.getRestaurantMenuItems(this.restaurantId).subscribe({
      next: menuItems => console.log(menuItems)
    });
  }

  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
  }
}
