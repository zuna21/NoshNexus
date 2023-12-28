import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuCardComponent } from 'src/app/_components/menu-card/menu-card.component';
import { IMenuCard } from 'src/app/_interfaces/IMenu';
import { Subscription, mergeMap } from 'rxjs';
import { MenuService } from 'src/app/_services/menu.service';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator'; 
import { ActivatedRoute, Params, Router } from '@angular/router';

@Component({
  selector: 'app-menus',
  standalone: true,
  imports: [
    CommonModule,
    MenuCardComponent,
    MatPaginatorModule
  ],
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css']
})
export class MenusComponent implements OnInit, OnDestroy {
  menus: IMenuCard[] = [];
  totalMenusNumber: number = 0;

  menuSub: Subscription | undefined;

  constructor(
    private menuService: MenuService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.setQueryParams();
    this.getMenus();
  }

  setQueryParams(pageIndex: number = 0) {
    const queryParams: Params = { pageIndex: pageIndex };
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
      mergeMap(params => this.menuService.getMenus(params['pageIndex']))
    ).subscribe({
      next: result => {
        this.totalMenusNumber = result.totalItems;
        this.menus = [...result.result];
      }
    });
  }

  handlePageEvent(event: PageEvent) {
    this.setQueryParams(event.pageIndex);
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
  }
}
