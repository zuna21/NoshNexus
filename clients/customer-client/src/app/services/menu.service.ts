import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { IMenu, IMenuCard } from '../interfaces/menu.interface';

const BASE_URL: string = `${environment.apiUrl}/menus`;

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(
    private http: HttpClient
  ) { }


  getRestaurantMenus(restaurantId: number): Observable<IMenuCard[]> {
    return this.http.get<IMenuCard[]>(`${BASE_URL}/get-restaurant-menus/${restaurantId}`);
  }

  getMenu(menuId: number): Observable<IMenu> {
    return this.http.get<IMenu>(`${BASE_URL}/get-menu/${menuId}`);
  }

}
