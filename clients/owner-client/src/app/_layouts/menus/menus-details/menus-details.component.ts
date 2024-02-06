import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { MatTabsModule } from '@angular/material/tabs';
import { MenuItemCreateComponent } from '../menu-item-create/menu-item-create.component';
import { MenuItemListComponent } from '../menu-item-list/menu-item-list.component';
import { ActivatedRoute, Params, Router, RouterLink } from '@angular/router';
import { Subscription, mergeMap, of } from 'rxjs';
import { IMenuDetails, IMenuItemCard } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { IMenuItemsQueryParams } from 'src/app/_interfaces/query_params.interface';
import { MENU_ITEMS_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import {MatButtonToggleModule} from '@angular/material/button-toggle'; 
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-menus-details',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatTabsModule,
    MenuItemCreateComponent,
    MenuItemListComponent,
    MatDialogModule,
    RouterLink,
    MatPaginatorModule,
    MatButtonToggleModule
  ],
  templateUrl: './menus-details.component.html',
  styleUrls: ['./menus-details.component.css'],
})
export class MenusDetailsComponent implements OnInit, OnDestroy {
  menu?: IMenuDetails;
  menuId: string = '';
  menuItemsQueryParams: IMenuItemsQueryParams = { ...MENU_ITEMS_QUERY_PARAMS };
  offer: "all" | "noSpecialOffer" | "specialOffer" = "all";

  menuSub?: Subscription;
  dialogRefSub?: Subscription;
  searchSub?: Subscription;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private router: Router,
    private searchBarService: SearchBarService,
  ) {}

  ngOnInit(): void {
    this.getMenu();
    this.setQueryParams();
    this.onSearch();
  }

  setQueryParams(): void {
    const queryParams: Params = { ...this.menuItemsQueryParams };

    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams,
    });
  }

  getMenu() {
    this.menuId = this.activatedRoute.snapshot.params['id'];
    this.menuSub = this.activatedRoute.queryParams.pipe(
      mergeMap(_ => this.menuService.getMenu(this.menuId, this.menuItemsQueryParams))
    ).subscribe({
      next: menu => this.menu = {...menu}
    });
  }

  onDeleteMenu() {
    if (!this.menu) return;
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to delete ${this.menu.name}?`,
    };

    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    this.dialogRefSub = dialogRef
      .afterClosed()
      .pipe(
        mergeMap((answer) => {
          if (!answer || !this.menu) return of(null);
          return this.menuService.delete(this.menu.id);
        })
      )
      .subscribe({
        next: (deletedMenuId) => {
          if (!deletedMenuId) return;
          this.router.navigateByUrl(`/menus`);
        },
      });
  }

  onPaginator(event: PageEvent) {
    this.menuItemsQueryParams = {
      ...this.menuItemsQueryParams,
      pageIndex: event.pageIndex
    }
    this.setQueryParams();
  }

  onSearch() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: search => {
        this.menuItemsQueryParams = {
          ...this.menuItemsQueryParams,
          pageIndex: 0,
          search: search === '' ? null : search
        };
        this.setQueryParams();
      }
    });
  }

  menuItemCreated(menuItem: IMenuItemCard) {
    if (!this.menu) return;
    this.menu.menuItems.result = [...this.menu.menuItems.result, menuItem];
  }

  onOfferChange() {
    this.menuItemsQueryParams = {
      ...this.menuItemsQueryParams,
      pageIndex: 0,
      offer: this.offer
    };

    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
    this.dialogRefSub?.unsubscribe();
    this.searchSub?.unsubscribe();
  }
}
