import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITable, ITableCard } from '../_interfaces/ITable';
import { ITablesQueryParams } from '../_interfaces/query_params.interface';
import { IPagedList } from '../_interfaces/IPagedList';
import { environment } from 'src/environments/environment';

const OWNER_URL: string = `${environment.apiUrl}/owner/tables`;

@Injectable({
  providedIn: 'root',
})
export class TableService {
  constructor(private http: HttpClient) {}

  create(restaurantTables: ITableCard[]): Observable<boolean> {
    return this.http.post<boolean>(`${OWNER_URL}/create`, restaurantTables);
  }

  delete(tableId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${OWNER_URL}/delete/${tableId}`);
  }

  getOwnerTables(tablesQueryParams: ITablesQueryParams): Observable<IPagedList<ITableCard[]>> {
    let params = new HttpParams();
    params = params.set('pageIndex', tablesQueryParams.pageIndex);
    params = params.set('pageSize', tablesQueryParams.pageSize);
    if (tablesQueryParams.search) params = params.set('search', tablesQueryParams.search);
    if (tablesQueryParams.restaurant) params = params.set('restaurant', tablesQueryParams.restaurant);

    return this.http.get<IPagedList<ITableCard[]>>(`${OWNER_URL}/get-tables`, { params });
  }

  getAllRestaurantTableNames(restaurantId: number): Observable<ITable[]> {
    return this.http.get<ITable[]>(`${OWNER_URL}/get-all-restaurant-table-names/${restaurantId}`);
  }
}
