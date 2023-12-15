import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IMenuDetails } from 'src/app/_interfaces/IMenu';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MenuService } from 'src/app/_services/menu.service';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MenuItemRowComponent } from 'src/app/_components/menu-item-row/menu-item-row.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IMenuItemRow } from 'src/app/_interfaces/IMenuItem';
import { OrderStore } from 'src/app/_stores/order.store';
import { IOrder } from 'src/app/_interfaces/IOrder';

@Component({
  selector: 'app-menu-details',
  standalone: true,
  imports: [
    CommonModule,
    SearchBarComponent,
    MenuItemRowComponent,
    MatProgressSpinnerModule
  ],
  templateUrl: './menu-details.component.html',
  styleUrls: ['./menu-details.component.css']
})
export class MenuDetailsComponent implements OnInit, OnDestroy {
  menu?: IMenuDetails;
  menuId?: number;
  isImageNotLoaded: boolean = true;

  menuSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private menuService: MenuService,
    private orderStore: OrderStore
  ) {}

  ngOnInit(): void {
    this.getMenu();
  }

  getMenu() {
    this.menuId = this.activatedRoute.snapshot.params['menuId'];
    if (!this.menuId) return;
    this.menuSub = this.menuService.getMenu(this.menuId).subscribe({
      next: menu => this.menu = menu
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
    this.menuSub?.unsubscribe();
  }
}
