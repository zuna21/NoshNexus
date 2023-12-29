import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ITableCard } from 'src/app/_interfaces/ITable';
import { TableService } from 'src/app/_services/table.service';
import { Subscription, mergeMap, of } from 'rxjs';
import { TableCardComponent } from 'src/app/_components/table-card/table-card.component';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { ITablesQueryParams } from 'src/app/_interfaces/query_params.interface';
import { TABLES_QUERY_PARAMS } from 'src/app/_default_values/default_query_params';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SearchBarService } from 'src/app/_components/search-bar/search-bar.service';
import {MatSelectModule} from '@angular/material/select'; 
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';

@Component({
  selector: 'app-tables',
  standalone: true,
  imports: [
    CommonModule, 
    TableCardComponent, 
    MatDialogModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatSelectModule
  ],
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.css'],
})
export class TablesComponent implements OnInit, OnDestroy {
  tables: ITableCard[] = [];
  tablesQueryParams: ITablesQueryParams = {...TABLES_QUERY_PARAMS};
  totalItems: number = 0;
  restaurants: IRestaurantSelect[] = [{id: -1, name: 'All Restaurants'}];

  tableSub?: Subscription;
  dialogRefSub?: Subscription;
  searchSub?: Subscription;
  restaurantSub?: Subscription;
  
  constructor(
    private tableService: TableService, 
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private searchBarService: SearchBarService,
    private restaurantService: RestaurantService
  ) {}

  ngOnInit(): void {
    this.setQueryParams();
    this.getTables();
    this.onSearch();
    this.getRestaurants();
  }

  setQueryParams() {
    const queryParams: Params = {...this.tablesQueryParams};
    this.router.navigate(
      [], 
      {
        relativeTo: this.activatedRoute,
        queryParams, 
      }
    );
  }

  getTables() {
    this.tableSub = this.activatedRoute.queryParams.pipe(
      mergeMap(_ => this.tableService.getOwnerTables(this.tablesQueryParams))
    ).subscribe({
      next: result => {
        if (!result) return;
        this.tables = [...result.result];
        this.totalItems = result.totalItems;
      }
    });
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService.getOwnerRestaurantsForSelect().subscribe({
      next: restaurants => this.restaurants = [...this.restaurants, ...restaurants]
    });
  }

  onRemoveTable(tableId: number) {
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to remove this table?`,
    };
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    this.dialogRefSub = dialogRef.afterClosed().pipe(
      mergeMap(result => {
        if (!result) return of(null);
        return this.tableService.delete(tableId);
      })
    ).subscribe({
      next: isDeleted => {
        if (!isDeleted) return;
        this.tables = this.tables.filter(x => x.id !== tableId);
        this.snackBar.open("Successfully deleted table", "Ok", {duration: 2000, panelClass: 'success-snackbar'})
      }
    });
  }

  onPaginator(event: PageEvent) {
    this.tablesQueryParams = {
      ...this.tablesQueryParams,
      pageIndex: event.pageIndex,
      pageSize: event.pageSize
    }
    this.setQueryParams();
  }

  onSearch() {
    this.searchSub = this.searchBarService.searchQuery$.subscribe({
      next: search => {
        this.tablesQueryParams = {
          ...this.tablesQueryParams,
          pageIndex: 0,
          search: search === '' ? null : search
        };

        this.setQueryParams();
      }
    });
  }

  onChangeRestaurant(restaurantId: number) {
    this.tablesQueryParams = {
      ...this.tablesQueryParams,
      restaurant: restaurantId === -1 ? null : restaurantId,
      pageIndex: 0
    };

    this.setQueryParams();
  }

  ngOnDestroy(): void {
    this.tableSub?.unsubscribe();
    this.dialogRefSub?.unsubscribe();
    this.searchSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
  }
}
