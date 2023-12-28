import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ICreateMenu, ICreateMenuItem, IEditMenu, IEditMenuItem, IGetMenuEdit, IGetMenuItem, IGetMenuItemEdit, IMenuCard, IMenuDetails, IMenuItemCard } from '../_interfaces/IMenu';
import { IImageCard } from '../_interfaces/IImage';
import { IPagedList } from '../_interfaces/IPagedList';
import { IMenusQueryParams } from '../_interfaces/query_params.interface';

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

  getMenus(menusQueryParams: IMenusQueryParams): Observable<IPagedList<IMenuCard[]>> {
    let params = new HttpParams();
    params = params.set('pageIndex', menusQueryParams.pageIndex);
    params = params.set('activity', menusQueryParams.activity);
    if (menusQueryParams.search) params = params.set('search', menusQueryParams.search);
    if (menusQueryParams.restaurant) params = params.set('restaurant', menusQueryParams.restaurant)

    return this.http.get<IPagedList<IMenuCard[]>>(`http://localhost:5000/api/owner/menus/get-menus`, { params });
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

  getMenuItemEdit(menuItemId: string): Observable<IGetMenuItemEdit> {
    return this.http.get<IGetMenuItemEdit>(`http://localhost:5000/api/owner/menuitems/get-menu-item-edit/${menuItemId}`);
  }
}
