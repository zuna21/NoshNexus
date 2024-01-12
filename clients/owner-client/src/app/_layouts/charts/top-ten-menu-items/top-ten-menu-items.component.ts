import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PieChartComponent } from 'src/app/_components/charts/pie-chart/pie-chart.component';
import { IPieChart } from 'src/app/_interfaces/IChart';
import { Subscription } from 'rxjs';
import { ChartService } from 'src/app/_services/chart.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MenuService } from 'src/app/_services/menu.service';
import { IRestaurantMenuForSelect } from 'src/app/_interfaces/IMenu';
import {MatSelectModule} from '@angular/material/select'; 
import { FormsModule } from '@angular/forms';
import { ITopTenMenuItemsParams } from 'src/app/_interfaces/query_params.interface';

@Component({
  selector: 'app-top-ten-menu-items',
  standalone: true,
  imports: [
    CommonModule,
    PieChartComponent,
    MatSelectModule,
    FormsModule
  ],
  templateUrl: './top-ten-menu-items.component.html',
  styleUrls: ['./top-ten-menu-items.component.css']
})
export class TopTenMenuItemsComponent implements OnInit, OnDestroy {
  chartData: IPieChart = {
    data: [],
    labels: []
  }; 
  restaurantId?: number;
  menus: IRestaurantMenuForSelect[] = [{id: -1, name: "All Menus"}];
  selectedMenu: number = -1;
  topTenMenuItemsParams: ITopTenMenuItemsParams = {
    menu: null
  };

  chartDataSub?: Subscription;
  menuSub?: Subscription;

  constructor(
    private chartService: ChartService,
    private activatedRoute: ActivatedRoute,
    private menuService: MenuService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getData();
    this.getMenus();
    this.setQueryParams();
  } 

  setQueryParams() {
    const queryParams: Params = { ...this.topTenMenuItemsParams };
  
    this.router.navigate(
      [], 
      {
        relativeTo: this.activatedRoute,
        queryParams, 
      }
    );
  }

  getMenus() {
    if (!this.restaurantId) return;
    this.menuSub = this.menuService.getRestaurantMenusForSelect(this.restaurantId).subscribe({
      next: menus => this.menus = [...this.menus, ...menus]
    });
  }

  getData() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;

    this.chartDataSub = this.chartService.getTopTenMenuItems(this.restaurantId).subscribe({
      next: data => this.chartData = {...data}
    });
  }

  onChangeMenu() {
    this.topTenMenuItemsParams = {
      ...this.topTenMenuItemsParams,
      menu: this.selectedMenu === -1 ? null : this.selectedMenu
    };

    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.chartDataSub?.unsubscribe();
    this.menuSub?.unsubscribe();
  }
}
