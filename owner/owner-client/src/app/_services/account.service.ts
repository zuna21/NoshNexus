import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IAccount } from '../_interfaces/IAccount';

const BASE_URL: string = `${environment.apiUrl}/account`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  getOwner(): Observable<IAccount> {
    return this.http.get<IAccount>(`${BASE_URL}/get-owner`);
  }
}
