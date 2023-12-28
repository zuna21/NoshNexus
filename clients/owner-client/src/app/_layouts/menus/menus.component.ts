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

@Component({
  selector: 'app-menus',
  standalone: true,
  imports: [
    CommonModule,
    MenuCardComponent,
    MatPaginatorModule,
    MatButtonToggleModule,
    FormsModule
  ],
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css']
})
export class MenusComponent implements OnInit, OnDestroy {
  menus: IMenuCard[] = [];
  totalMenusNumber: number = 0;
  search: string | null = null;
  menusQueryParams: IMenusQueryParams = {
    pageIndex: 0,
    search: null,
    activity: "all"
  };
  activity: "all" | "active" | "inactive" = "all";

  menuSub?: Subscription;
  searchSub?: Subscription;

  constructor(
    private menuService: MenuService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private searchBarService: SearchBarService
  ) { }

  ngOnInit(): void {
    this.setQueryParams();
    this.getMenus();
    this.searchBarResult();
  }

  setQueryParams() {
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
        this.totalMenusNumber = result.totalItems;
        this.menus = [...result.result];
      }
    });
  }

  searchBarResult() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: search => {
        this.menusQueryParams = {
          pageIndex: 0,
          activity: this.activity,
          search: search ? search : null
        }
        this.setQueryParams(); // uvijek vraca na prvu stranicu
      }
    });
  }

  activityChanged() {
    this.menusQueryParams = {
      ...this.menusQueryParams,
      activity: this.activity
    };
    this.setQueryParams();
  }

  handlePageEvent(event: PageEvent) {
    this.menusQueryParams.pageIndex = event.pageIndex;
    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
    this.searchSub?.unsubscribe();
  }
}
