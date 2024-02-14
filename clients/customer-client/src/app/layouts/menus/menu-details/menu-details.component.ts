import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MenuItemCardComponent } from '../../../components/menu-item-card/menu-item-card.component';
import { IMenu } from '../../../interfaces/menu.interface';
import { MenuService } from '../../../services/menu.service';
import { Subscription, mergeMap, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { IMenuItemCard } from '../../../interfaces/menu-item.interface';
import { MenuItemService } from '../../../services/menu-item.service';
import { OrderBottomNavigationComponent } from '../../../components/order-bottom-navigation/order-bottom-navigation.component';
import { IMenuItemsQueryParams } from '../../../query_params/menu-items.query-params';
import { MENU_ITEMS_QUERY_PARAMS } from '../../../query_params/default_value/menu-items.defaultQP';
import { ScrollService } from '../../main/scroll.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-menu-details',
  standalone: true,
  imports: [
    MatIconModule,
    MenuItemCardComponent,
    OrderBottomNavigationComponent,
    MatProgressSpinnerModule
  ],
  templateUrl: './menu-details.component.html',
  styleUrl: './menu-details.component.css'
})
export class MenuDetailsComponent implements OnInit, OnDestroy {
  menu?: IMenu;
  menuId?: number;
  menuItems: IMenuItemCard[] = [];
  queryParams: IMenuItemsQueryParams = {...MENU_ITEMS_QUERY_PARAMS};
  hasMoreMenuItems: boolean = true;

  menuSub?: Subscription;
  menuItemSub?: Subscription;
  scrollSub?: Subscription;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute,
    private menuItemService: MenuItemService,
    private scrollService: ScrollService
  ) {}

  ngOnInit(): void {
    this.getMenu();
    this.getMenuItems();
    this.scrollToBottom();
  }

  getMenu() {
    this.menuId = parseInt(this.activatedRoute.snapshot.params['menuId']);
    if (!this.menuId) return;
    this.menuSub = this.menuService.getMenu(this.menuId).subscribe({
      next: menu => this.menu = menu
    });
  }

  scrollToBottom() {
    this.scrollSub = this.scrollService.scolledToBottom$.pipe(
      mergeMap(_ => {
        if (!this.menuId || !this.hasMoreMenuItems) return of(null);
        const pageIndex = ++this.queryParams.pageIndex;
        this.queryParams = {
          ...this.queryParams,
          pageIndex: pageIndex
        };
        return this.menuItemService.getMenuMenuItems(this.menuId, this.queryParams);
      })
    ).subscribe({
      next: menuItems => {
        if (!menuItems) return;
        if (menuItems.length < this.queryParams.pageSize) this.hasMoreMenuItems = false;
        this.menuItems = [...this.menuItems, ...menuItems];
      }
    })
  }

  getMenuItems() {
    this.menuId = parseInt(this.activatedRoute.snapshot.params['menuId']);
    if (!this.menuId) return;
    this.menuItemSub = this.menuItemService.getMenuMenuItems(this.menuId, this.queryParams).subscribe({
      next: menuItems => {
        if (menuItems.length < this.queryParams.pageSize) this.hasMoreMenuItems = false;
        this.menuItems = [...menuItems];
      }
    });
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
    this.menuItemSub?.unsubscribe();
    this.scrollSub?.unsubscribe();
  }
}
