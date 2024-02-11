import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MenuItemCardComponent } from '../../../components/menu-item-card/menu-item-card.component';
import { IMenu } from '../../../interfaces/menu.interface';
import { MenuService } from '../../../services/menu.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { IMenuItemCard } from '../../../interfaces/menu-item.interface';
import { MenuItemService } from '../../../services/menu-item.service';
import { OrderBottomNavigationComponent } from '../../../components/order-bottom-navigation/order-bottom-navigation.component';

@Component({
  selector: 'app-menu-details',
  standalone: true,
  imports: [
    MatIconModule,
    MenuItemCardComponent,
    OrderBottomNavigationComponent
  ],
  templateUrl: './menu-details.component.html',
  styleUrl: './menu-details.component.css'
})
export class MenuDetailsComponent implements OnInit, OnDestroy {
  menu?: IMenu;
  menuId?: number;
  menuItems: IMenuItemCard[] = [];

  menuSub?: Subscription;
  menuItemSub?: Subscription;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute,
    private menuItemService: MenuItemService
  ) {}

  ngOnInit(): void {
    this.getMenu();
    this.getMenuItems();
  }

  getMenu() {
    this.menuId = parseInt(this.activatedRoute.snapshot.params['menuId']);
    if (!this.menuId) return;
    this.menuSub = this.menuService.getMenu(this.menuId).subscribe({
      next: menu => this.menu = menu
    });
  }

  getMenuItems() {
    this.menuId = parseInt(this.activatedRoute.snapshot.params['menuId']);
    if (!this.menuId) return;
    this.menuItemSub = this.menuItemService.getMenuMenuItems(this.menuId).subscribe({
      next: menuItems => this.menuItems = [...menuItems]
    });
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
    this.menuItemSub?.unsubscribe();
  }
}
