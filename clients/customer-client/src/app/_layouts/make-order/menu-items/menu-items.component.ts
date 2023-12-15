import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MenuItemRowComponent } from 'src/app/_components/menu-item-row/menu-item-row.component';
import { IMenuItemRow } from 'src/app/_interfaces/IMenuItem';
import { Subscription } from 'rxjs';
import { MenuItemService } from 'src/app/_services/menu-item.service';
import { ActivatedRoute } from '@angular/router';
import { OrderStore } from 'src/app/_stores/order.store';
import { IOrder } from 'src/app/_interfaces/IOrder';

@Component({
  selector: 'app-menu-items',
  standalone: true,
  imports: [
    CommonModule,
    SearchBarComponent,
    MenuItemRowComponent
  ],
  templateUrl: './menu-items.component.html',
  styleUrls: ['./menu-items.component.css']
})
export class MenuItemsComponent implements OnInit, OnDestroy {
  menuItems: IMenuItemRow[] = [];
  restaurantId?: number;

  menuItemSub?: Subscription;
  searchMenuItemSub?: Subscription;

  constructor(
    private menuItemService: MenuItemService,
    private activatedRoute: ActivatedRoute,
    private orderStore: OrderStore
  ) {}

  ngOnInit(): void {
    this.getMenuItems();
  }

  getMenuItems() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;
    this.menuItemSub = this.menuItemService.getRestaurantMenuItems(this.restaurantId).subscribe({
      next: menuItems => this.menuItems = menuItems
    });
  }

  onSearch(sq: string) {
    if (!this.restaurantId) return;
    this.searchMenuItemSub = this.menuItemService.getRestaurantMenuItems(this.restaurantId, sq)
      .subscribe({
        next: menuItems => this.menuItems = [...menuItems]
      });
  }

  addToOrder(menuItem: IMenuItemRow) {
    const oldOrder = this.orderStore.getOrder();
    const newOrder: IOrder = {
      ...oldOrder,
      menuItems: [...oldOrder.menuItems, menuItem]
    };
    this.orderStore.setOrder(newOrder);
  }

  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
  }
}
