import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IMenuCard } from '../_interfaces/IMenu';

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
}
