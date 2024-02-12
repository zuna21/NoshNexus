import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItemService } from '../../services/menu-item.service';
import { IMenuItemCard } from '../../interfaces/menu-item.interface';
import { Subscription, mergeMap, of } from 'rxjs';
import { MenuItemCardComponent } from '../../components/menu-item-card/menu-item-card.component';
import { ScrollService } from '../main/scroll.service';
import { MENU_ITEMS_QUERY_PARAMS } from '../../query_params/default_value/menu-items.defaultQP';
import { IMenuItemsQueryParams } from '../../query_params/menu-items.query-params';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-menu-items',
  standalone: true,
  imports: [MenuItemCardComponent, MatProgressSpinnerModule],
  templateUrl: './menu-items.component.html',
  styleUrl: './menu-items.component.css',
})
export class MenuItemsComponent implements OnInit, OnDestroy {
  restaurantId?: number;
  menuItems = signal<IMenuItemCard[]>([]);
  queryParams: IMenuItemsQueryParams = {...MENU_ITEMS_QUERY_PARAMS};
  hasMoreMenuItems: boolean = true;

  menuItemSub?: Subscription;
  scrollSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private menuItemService: MenuItemService,
    private scrollSerice: ScrollService
  ) {}

  ngOnInit(): void {
    this.getMenuItems();
    this.onScrollToBottom();
  }

  getMenuItems() {
    this.restaurantId = parseInt(
      this.activatedRoute.snapshot.params['restaurantId']
    );
    if (!this.restaurantId) return;
    this.menuItemSub = this.menuItemService
      .getRestaurantMenuItems(this.restaurantId, this.queryParams)
      .subscribe({
        next: (menuItems) => this.menuItems.set(menuItems),
      });
  }

  onScrollToBottom() {
    this.scrollSub = this.scrollSerice.scolledToBottom$
      .pipe(
        mergeMap(() => {
          if (!this.hasMoreMenuItems || !this.restaurantId) return of(null);
          const pageIndex = ++this.queryParams.pageIndex;  // treba prvo ++ jer smo vec na pocetku uzeli kad je pageIndex 0
          const newQueryParams = {
            ...this.queryParams,
            pageIndex: pageIndex,
          };
          return this.menuItemService.getRestaurantMenuItems(
            this.restaurantId,
            newQueryParams
          );
        })
      )
      .subscribe({
        next: (menuItems) => {
          if (!menuItems) return;
          if (menuItems.length < this.queryParams.pageSize)
            this.hasMoreMenuItems = false;
          this.menuItems.update((value) => [...value, ...menuItems]);
        },
      });
  }

  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
    this.scrollSub?.unsubscribe();
  }
}
