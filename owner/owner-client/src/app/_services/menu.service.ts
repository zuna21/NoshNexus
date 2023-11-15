import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ICreateMenu, ICreateMenuItem, IEditMenu, IGetMenuEdit, IGetMenuItem, IMenuCard, IMenuDetails, IMenuItemCard, IMenuItemEdit } from '../_interfaces/IMenu';

const BASE_URL: string = `${environment.apiUrl}/menu`;

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(
    private http: HttpClient
  ) { }

  create(menu: ICreateMenu): Observable<number> {
    return this.http.post<number>(`http://localhost:5000/api/owner/menus/create`, menu);
  }

  update(menuId: string, menu: IEditMenu): Observable<number> {
    return this.http.put<number>(`http://localhost:5000/api/owner/menus/update/${menuId}`, menu);
  }

  createMenuItem(menuId: string, menuItem: ICreateMenuItem): Observable<IMenuItemCard> {
    return this.http.post<IMenuItemCard>(`http://localhost:5000/api/owner/menuitems/create/${menuId}`, menuItem);
  }

  getMenus(): Observable<IMenuCard[]> {
    return this.http.get<IMenuCard[]>(`http://localhost:5000/api/owner/menus/get-menus`);
  }

  getMenu(menuId: string): Observable<IMenuDetails> {
    return this.http.get<IMenuDetails>(`http://localhost:5000/api/owner/menus/get-menu/${menuId}`);
  }

  getMenuEdit(menuId: string): Observable<IGetMenuEdit> {
    return this.http.get<IGetMenuEdit>(`http://localhost:5000/api/owner/menus/get-menu-edit/${menuId}`);
  }

  getMenuItem(menuItemId: string): Observable<IGetMenuItem> {
    return this.http.get<IGetMenuItem>(`http://localhost:5000/api/owner/menuitems/get-menu-item/${menuItemId}`);
  }

  getOwnerMenuItemEdit(menuItemId: string): Observable<IMenuItemEdit> {
    return this.http.get<IMenuItemEdit>(`${BASE_URL}/get-owner-menu-item-edit/${menuItemId}`);
  }
}
