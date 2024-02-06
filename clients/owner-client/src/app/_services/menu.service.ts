import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  ICreateMenu,
  ICreateMenuItem,
  IEditMenu,
  IEditMenuItem,
  IGetMenuEdit,
  IGetMenuItem,
  IGetMenuItemEdit,
  IMenuCard,
  IMenuDetails,
  IMenuItemCard,
  IRestaurantMenuForSelect,
} from '../_interfaces/IMenu';
import { IImageCard } from '../_interfaces/IImage';
import { IPagedList } from '../_interfaces/IPagedList';
import {
  IMenuItemsQueryParams,
  IMenusQueryParams,
} from '../_interfaces/query_params.interface';
import { environment } from 'src/environments/environment';

const OWNER_MENU_URL: string = `${environment.apiUrl}/owner/menus`;
const OWNER_MENU_ITEM_URL: string = `${environment.apiUrl}/owner/menuItems`;
const EMPLOYEE_MENU_ITEM_URL: string = `${environment.apiUrl}/employee/menuItems`;

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  constructor(private http: HttpClient) {}

  create(menu: ICreateMenu): Observable<number> {
    return this.http.post<number>(
      `${OWNER_MENU_URL}/create`,
      menu
    );
  }

  update(menuId: string, menu: IEditMenu): Observable<number> {
    return this.http.put<number>(
      `${OWNER_MENU_URL}/update/${menuId}`,
      menu
    );
  }

  delete(menuId: number): Observable<number> {
    return this.http.delete<number>(
      `${OWNER_MENU_URL}/delete/${menuId}`
    );
  }

  updateMenuItem(
    menuItemId: string,
    menuItem: IEditMenuItem,
    isOwner: boolean = false
  ): Observable<number> {
    return this.http.put<number>(
      `${isOwner ? OWNER_MENU_ITEM_URL : EMPLOYEE_MENU_ITEM_URL}/update/${menuItemId}`,
      menuItem
    );
  }

  deleteMenuItemImage(menuItemImageId: string): Observable<number> {
    return this.http.delete<number>(
      `${OWNER_MENU_ITEM_URL}/delete-image/${menuItemImageId}`
    );
  }

  deleteMenuItem(menuItemId: number): Observable<number> {
    return this.http.delete<number>(
      `${OWNER_MENU_ITEM_URL}/delete/${menuItemId}`
    );
  }

  uploadMenuItemProfileImage(
    menuItemId: string,
    image: FormData
  ): Observable<IImageCard> {
    return this.http.post<IImageCard>(
      `${OWNER_MENU_ITEM_URL}/upload-profile-image/${menuItemId}`,
      image
    );
  }

  createMenuItem(
    menuId: string,
    menuItem: ICreateMenuItem,
    isOwner: boolean = false
  ): Observable<IMenuItemCard> {
    return this.http.post<IMenuItemCard>(
      `${isOwner ? OWNER_MENU_ITEM_URL : EMPLOYEE_MENU_ITEM_URL}/create/${menuId}`,
      menuItem
    );
  }

  getMenus(
    menusQueryParams: IMenusQueryParams
  ): Observable<IPagedList<IMenuCard[]>> {
    let params = new HttpParams();
    params = params.set('pageIndex', menusQueryParams.pageIndex);
    params = params.set('activity', menusQueryParams.activity);
    if (menusQueryParams.search)
      params = params.set('search', menusQueryParams.search);
    if (menusQueryParams.restaurant)
      params = params.set('restaurant', menusQueryParams.restaurant);

    return this.http.get<IPagedList<IMenuCard[]>>(
      `${OWNER_MENU_URL}/get-menus`,
      { params }
    );
  }

  getMenu(
    menuId: string,
    menuItemsQueryParams: IMenuItemsQueryParams
  ): Observable<IMenuDetails> {
    let params = new HttpParams();
    params = params.set('pageIndex', menuItemsQueryParams.pageIndex);
    params = params.set('offer', menuItemsQueryParams.offer);
    if (menuItemsQueryParams.search) params = params.set('search', menuItemsQueryParams.search);

    return this.http.get<IMenuDetails>(
      `${OWNER_MENU_URL}/get-menu/${menuId}`,
      { params }
    );
  }

  getMenuEdit(menuId: string): Observable<IGetMenuEdit> {
    return this.http.get<IGetMenuEdit>(
      `${OWNER_MENU_URL}/get-menu-edit/${menuId}`
    );
  }

  getMenuItem(menuItemId: string, isOwner: boolean = false): Observable<IGetMenuItem> {
    return this.http.get<IGetMenuItem>(
      `${isOwner ? OWNER_MENU_ITEM_URL : EMPLOYEE_MENU_ITEM_URL}/get-menu-item/${menuItemId}`
    );
  }

  getMenuItemEdit(menuItemId: string, isOwner: boolean = false): Observable<IGetMenuItemEdit> {
    return this.http.get<IGetMenuItemEdit>(
      `${isOwner ? OWNER_MENU_ITEM_URL: EMPLOYEE_MENU_ITEM_URL}/get-menu-item-edit/${menuItemId}`
    );
  }

  getRestaurantMenusForSelect(restaurantId: number): Observable<IRestaurantMenuForSelect[]> {
    return this.http.get<IRestaurantMenuForSelect[]>(`${OWNER_MENU_URL}/get-restaurant-menus-for-select/${restaurantId}`);
  }
}
