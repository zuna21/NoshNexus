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

  getRestaurantMenuItems(restaurantId: number, sq: string = ''): Observable<IMenuItemRow[]> {
    return this.http.get<IMenuItemRow[]>(`http://localhost:5000/api/menuitems/get-restaurant-menu-items/${restaurantId}?sq=${sq}`);
  }
}
