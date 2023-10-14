import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IMenuCard, IMenuDetails, IMenuEdit } from '../_interfaces/IMenu';

const BASE_URL: string = `${environment.apiUrl}/menu`;

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(
    private http: HttpClient
  ) { }

  getOwnerMenus(): Observable<IMenuCard[]> {
    return this.http.get<IMenuCard[]>(`${BASE_URL}/get-owner-menus`);
  }

  getOwnerMenuDetails(menuId: string): Observable<IMenuDetails> {
    return this.http.get<IMenuDetails>(`${BASE_URL}/get-owner-menu-details/${menuId}`);
  }

  getOwnerMenuEdit(menuId: string): Observable<IMenuEdit> {
    return this.http.get<IMenuEdit>(`${BASE_URL}/get-owner-menu-edit/${menuId}`);
  }
}
