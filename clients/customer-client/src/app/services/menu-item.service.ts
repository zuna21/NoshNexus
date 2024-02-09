import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { IMenuItemCard } from '../interfaces/menu-item.interface';

const BASE_URL: string = `${environment.apiUrl}/menuItems`;

@Injectable({
  providedIn: 'root'
})
export class MenuItemService {

  constructor(
    private http: HttpClient
  ) { }

  getRestaurantMenuItems(restaurantId: number): Observable<IMenuItemCard[]> {
    return this.http.get<IMenuItemCard[]>(`${BASE_URL}/get-restaurant-menu-items/${restaurantId}`);
  } 

  getMenuMenuItems(menuId: number): Observable<IMenuItemCard[]> {
    return this.http.get<IMenuItemCard[]>(`${BASE_URL}/get-menu-menu-items/${menuId}`);
  }
}
