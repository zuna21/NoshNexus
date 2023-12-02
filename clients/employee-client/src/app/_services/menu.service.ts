import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ICreateMenu, ICreateMenuItem, IEditMenu, IEditMenuItem, IGetMenuEdit, IGetMenuItem, IGetMenuItemEdit, IMenuCard, IMenuDetails, IMenuItemCard } from '../_interfaces/IMenu';
import { IImageCard } from '../_interfaces/IImage';

const BASE_URL: string = `${environment.apiUrl}/menu`;

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(
    private http: HttpClient
  ) { }

  create(menu: ICreateMenu): Observable<number> {
    return this.http.post<number>(`http://localhost:5000/api/employee/menus/create`, menu);
  }

  update(menuId: string, menu: IEditMenu): Observable<number> {
    return this.http.put<number>(`http://localhost:5000/api/owner/menus/update/${menuId}`, menu);
  }

  delete(menuId: number): Observable<number> {
    return this.http.delete<number>(`http://localhost:5000/api/owner/menus/delete/${menuId}`);
  }

  updateMenuItem(menuItemId: string, menuItem: IEditMenuItem): Observable<number> {
    return this.http.put<number>(`http://localhost:5000/api/owner/menuitems/update/${menuItemId}`, menuItem);
  }

  deleteMenuItemImage(menuItemImageId: string): Observable<number> {
    return this.http.delete<number>(`http://localhost:5000/api/owner/menuitems/delete-image/${menuItemImageId}`);
  }

  deleteMenuItem(menuItemId: number): Observable<number> {
    return this.http.delete<number>(`http://localhost:5000/api/owner/menuitems/delete/${menuItemId}`);
  }

  uploadMenuItemProfileImage(menuItemId: string, image: FormData): Observable<IImageCard> {
    return this.http.post<IImageCard>(`http://localhost:5000/api/owner/menuitems/upload-profile-image/${menuItemId}`, image);
  }

  createMenuItem(menuId: string, menuItem: ICreateMenuItem): Observable<IMenuItemCard> {
    return this.http.post<IMenuItemCard>(`http://localhost:5000/api/owner/menuitems/create/${menuId}`, menuItem);
  }

  getMenus(): Observable<IMenuCard[]> {
    return this.http.get<IMenuCard[]>(`http://localhost:5000/api/employee/menus/get-menus`);
  }

  getMenu(menuId: string): Observable<IMenuDetails> {
    return this.http.get<IMenuDetails>(`http://localhost:5000/api/employee/menus/get-menu/${menuId}`);
  }

  getMenuEdit(menuId: string): Observable<IGetMenuEdit> {
    return this.http.get<IGetMenuEdit>(`http://localhost:5000/api/owner/menus/get-menu-edit/${menuId}`);
  }

  getMenuItem(menuItemId: string): Observable<IGetMenuItem> {
    return this.http.get<IGetMenuItem>(`http://localhost:5000/api/employee/menuitems/get-menu-item/${menuItemId}`);
  }

  getMenuItemEdit(menuItemId: string): Observable<IGetMenuItemEdit> {
    return this.http.get<IGetMenuItemEdit>(`http://localhost:5000/api/owner/menuitems/get-menu-item-edit/${menuItemId}`);
  }
}
