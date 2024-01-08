import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserCard } from '../_interfaces/IUser';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPagedList } from '../_interfaces/IPagedList';
import { IBlockedCustomersParams } from '../_interfaces/query_params.interface';

@Injectable({
  providedIn: 'root'
})
export class SettingService {

  constructor(
    private http: HttpClient
  ) { }

  getBlockedCustomers(blockedCustomersQueryParams: IBlockedCustomersParams): Observable<IPagedList<IUserCard[]>> {
    let params = new HttpParams();

    params = params.set('pageIndex', blockedCustomersQueryParams.pageIndex);
    if (blockedCustomersQueryParams.restaurant) params = params.set('restaurant', blockedCustomersQueryParams.restaurant);
    if (blockedCustomersQueryParams.search) params = params.set('search', blockedCustomersQueryParams.search);

    return this.http.get<IPagedList<IUserCard[]>>(`http://localhost:5000/api/owner/settings/get-owner-blocked-customers`, { params });
  }

  unblockCustomer(customerId: number): Observable<number> {
    return this.http.delete<number>(`http://localhost:5000/api/owner/settings/unblock-customer/${customerId}`);
  } 
}
