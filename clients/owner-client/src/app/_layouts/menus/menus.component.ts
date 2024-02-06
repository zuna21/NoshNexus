import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuCardComponent } from 'src/app/_components/menu-card/menu-card.component';
import { IMenuCard } from 'src/app/_interfaces/IMenu';
import { Subscription, mergeMap } from 'rxjs';
import { MenuService } from 'src/app/_services/menu.service';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator'; 
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { FormsModule } from '@angular/forms';
import { IMenusQueryParams } from 'src/app/_interfaces/query_params.interface';
import {MatSelectModule} from '@angular/material/select'; 
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { MatButtonModule } from '@angular/material/button';
import { MENUS_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { RestaurantStore } from 'src/app/_store/restaurant.store';

@Component({
  selector: 'app-menus',
  standalone: true,
  imports: [
    CommonModule,
    MenuCardComponent,
    MatPaginatorModule,
    MatButtonToggleModule,
    FormsModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css']
})
export class MenusComponent implements OnInit, OnDestroy {
  menus: IMenuCard[] = [];
  totalMenusNumber: number = 0;
  menusQueryParams: IMenusQueryParams = {...MENUS_QUERY_PARAMS};
  activity: "all" | "active" | "inactive" = "all";
  restaurants: IRestaurantSelect[] = [{id: -1, name: 'All Restaurants'}];
  selectedRestaurant: number = -1;
  canResetFilters: boolean = false;

  menuSub?: Subscription;
  searchSub?: Subscription;

  constructor(
    private menuService: MenuService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private searchBarService: SearchBarService,
    private restaurantService: RestaurantService,
    private restaurantStore: RestaurantStore
  ) { }

  ngOnInit(): void {
    this.setQueryParams();
    this.getRestaurants();
    this.getMenus();
    this.searchBarResult();
  }

  getRestaurants() {
    const restaurantsFromStore = this.restaurantStore.getRestaurantsForSelect();
    if (restaurantsFromStore.length <= 0) {
      this.restaurantService.getOwnerRestaurantsForSelect().subscribe({
        next: restaurants => {
          this.restaurantStore.setRestaurantsForSelect(restaurants);
          this.restaurants = [...this.restaurants, ...restaurants]
        }
      });
    } else {
      this.restaurants = [...this.restaurants, ...restaurantsFromStore];
    }
  }

  setQueryParams() {
    if (JSON.stringify(this.menusQueryParams) === JSON.stringify(MENUS_QUERY_PARAMS)) this.canResetFilters = false;
    else this.canResetFilters = true;

    const queryParams: Params = {...this.menusQueryParams};
    this.router.navigate(
      [], 
      {
        relativeTo: this.activatedRoute,
        queryParams,
      }
    );

  }

  getMenus() {
    this.menuSub = this.activatedRoute.queryParams.pipe(
      mergeMap(_ => this.menuService.getMenus(this.menusQueryParams))
    ).subscribe({
      next: result => {
        if (!result) return;
        this.totalMenusNumber = result.totalItems;
        this.menus = [...result.result];
      }
    });
  }

  searchBarResult() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: search => {
        this.menusQueryParams = {
          ...this.menusQueryParams,
          pageIndex: 0,
          search: search ? search : null
        };
  
        this.setQueryParams(); // uvijek vraca na prvu stranicu
      }
    });
  }

  activityChanged() {
    this.menusQueryParams = {
      ...this.menusQueryParams,
      pageIndex: 0,
      activity: this.activity
    };
    this.setQueryParams();
  }

  restaurantChanged() {
    this.menusQueryParams = {
      ...this.menusQueryParams,
      pageIndex: 0,
      restaurant: this.selectedRestaurant === -1 ? null : this.selectedRestaurant
    };
    this.setQueryParams();
  }

  handlePageEvent(event: PageEvent) {
    this.menusQueryParams.pageIndex = event.pageIndex;
    this.setQueryParams();
  }

  onResetFilters() {
    this.menusQueryParams = {...MENUS_QUERY_PARAMS};
    this.activity = 'all';
    this.selectedRestaurant = -1;
    this.searchBarService.searchQuery$.next('');
    this.setQueryParams();
  }

  onViewMore(menuId: string) {
    this.router.navigateByUrl(`/menus/${menuId}`);
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
    this.searchSub?.unsubscribe();
  }
}
