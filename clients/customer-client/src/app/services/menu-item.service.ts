import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { IMenuItemCard } from '../interfaces/menu-item.interface';
import { IMenuItemsQueryParams } from '../query_params/menu-items.query-params';

const BASE_URL: string = `${environment.apiUrl}/menuItems`;

@Injectable({
  providedIn: 'root',
})
export class MenuItemService {
  constructor(private http: HttpClient) {}

  getRestaurantMenuItems(
    restaurantId: number,
    queryParams: IMenuItemsQueryParams
  ): Observable<IMenuItemCard[]> {
    let params = new HttpParams();
    params = params.set('pageIndex', queryParams.pageIndex);
    params = params.set('search', queryParams.search);
    return this.http.get<IMenuItemCard[]>(
      `${BASE_URL}/get-restaurant-menu-items/${restaurantId}`,
      { params }
    );
  }

  getMenuMenuItems(menuId: number): Observable<IMenuItemCard[]> {
    return this.http.get<IMenuItemCard[]>(
      `${BASE_URL}/get-menu-menu-items/${menuId}`
    );
  }
}
