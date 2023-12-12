import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMenuCard, IMenuDetails } from '../_interfaces/IMenu';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(
    private http: HttpClient
  ) { }

  getMenus(restaurantId: number): Observable<IMenuCard[]> {
    return this.http.get<IMenuCard[]>(`http://localhost:5000/api/menus/get-menus/${restaurantId}`);
  }

  getMenu(menuId: number): Observable<IMenuDetails> {
    return this.http.get<IMenuDetails>(`http://localhost:5000/api/menus/get-menu/${menuId}`);
  }
}
