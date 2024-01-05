import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserCard } from '../_interfaces/IUser';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SettingService {

  constructor(
    private http: HttpClient
  ) { }

  getBlockedCustomers(): Observable<IUserCard[]> {
    return this.http.get<IUserCard[]>(`http://localhost:5000/api/owner/settings/get-owner-blocked-customers`);
  }
}
