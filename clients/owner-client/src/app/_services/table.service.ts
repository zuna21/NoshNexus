import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ITableCard } from '../_interfaces/ITable';
import { ITablesQueryParams } from '../_interfaces/query_params.interface';
import { IPagedList } from '../_interfaces/IPagedList';

const BASE_URL: string = `${environment.apiUrl}/table`;

@Injectable({
  providedIn: 'root',
})
export class TableService {
  constructor(private http: HttpClient) {}

  create(restaurantTables: ITableCard[]): Observable<boolean> {
    return this.http.post<boolean>(`http://localhost:5000/api/owner/tables/create`, restaurantTables);
  }

  delete(tableId: number): Observable<boolean> {
    return this.http.delete<boolean>(`http://localhost:5000/api/owner/tables/delete/${tableId}`);
  }

  getOwnerTables(tablesQueryParams: ITablesQueryParams): Observable<IPagedList<ITableCard[]>> {
    let params = new HttpParams();
    params = params.set('pageIndex', tablesQueryParams.pageIndex);
    params = params.set('pageSize', tablesQueryParams.pageSize);
    if (tablesQueryParams.search) params = params.set('search', tablesQueryParams.search);
    if (tablesQueryParams.restaurant) params = params.set('restaurant', tablesQueryParams.restaurant);

    return this.http.get<IPagedList<ITableCard[]>>(`http://localhost:5000/api/owner/tables/get-tables`, { params });
  }
}
