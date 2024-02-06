import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMenuCard, IMenuDetails } from 'src/app/_interfaces/IMenu';
import { IPagedList } from 'src/app/_interfaces/IPagedList';
import { environment } from 'src/environments/environment';
import { IMenusQueryParams } from '../_interfaces/query_params.interface';
import { ICreateMenu } from '../_interfaces/menu.interface';
import { IMenuItemsQueryParams } from 'src/app/_interfaces/query_params.interface';

const EMPLOYEE_URL: string = `${environment.apiUrl}/employee/menus`;

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  constructor(private http: HttpClient) {}

  getMenus(
    menusQueryParams: IMenusQueryParams
  ): Observable<IPagedList<IMenuCard[]>> {
    let params = new HttpParams();
    params = params.set('pageIndex', menusQueryParams.pageIndex);
    params = params.set('activity', menusQueryParams.activity);
    if (menusQueryParams.search)
      params = params.set('search', menusQueryParams.search);

    return this.http.get<IPagedList<IMenuCard[]>>(`${EMPLOYEE_URL}/get-menus`, {
      params,
    });
  }

  create(menu: ICreateMenu): Observable<number> {
    return this.http.post<number>(`${EMPLOYEE_URL}/create`, menu);
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
      `${EMPLOYEE_URL}/get-menu/${menuId}`,
      { params }
    );
  }



}
