import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  IAccount,
  IActivateAccount,
  IGetAccountDetails,
  ILogin,
} from '../interfaces/account.interface';

const BASE_URL: string = `${environment.apiUrl}/account`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  loginAsGuest(): Observable<IAccount> {
    return this.http.get<IAccount>(`${BASE_URL}/login-as-guest`);
  }

  login(loginAccount: ILogin): Observable<IAccount> {
    return this.http.post<IAccount>(`${BASE_URL}/login`, loginAccount);
  }

  activateAccount(activateAccount: IActivateAccount): Observable<boolean> {
    return this.http.post<boolean>(
      `${BASE_URL}/activate-account`,
      activateAccount
    );
  }

  getAccountDetails(): Observable<IGetAccountDetails> {
    return this.http.get<IGetAccountDetails>(`${BASE_URL}/get-account-details`);
  }
}
