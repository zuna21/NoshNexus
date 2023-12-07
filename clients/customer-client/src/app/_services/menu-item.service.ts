import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMenuItemRow } from '../_interfaces/IMenuItem';

@Injectable({
  providedIn: 'root'
})
export class MenuItemService {

  constructor(
    private http: HttpClient
  ) { }

  getRestaurantMenuItems(restaurantId: number): Observable<IMenuItemRow[]> {
    return this.http.get<IMenuItemRow[]>(`http://localhost:3000/menu-items/get-restaurant-menu-items/${restaurantId}`);
  }
}
