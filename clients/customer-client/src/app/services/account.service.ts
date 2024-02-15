import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import {
  IAccount,
  IActivateAccount,
  IEditAccount,
  IGetAccountDetails,
  IGetAccountEdit,
  ILogin,
} from '../interfaces/account.interface';
import { CookieService } from 'ngx-cookie-service';
import { IImageCard } from '../interfaces/image.interface';

const BASE_URL: string = `${environment.apiUrl}/account`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private user = new BehaviorSubject<IAccount | null>(null);
  user$ = this.user.asObservable();

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) {}

  loginAsGuest(): Observable<IAccount> {
    return this.http.get<IAccount>(`${BASE_URL}/login-as-guest`).pipe(
      map(user => {
        this.setUser(user);
        return user;
      })
    );
  }

  login(loginAccount: ILogin): Observable<IAccount> {
    return this.http.post<IAccount>(`${BASE_URL}/login`, loginAccount).pipe(
      map(user => {
        this.setUser(user);
        return user;
      })
    );
  }

  refreshCustomer(): Observable<IAccount> {
    return this.http.get<IAccount>(`${BASE_URL}/refresh-customer`).pipe(
      map(user => {
        if (user) this.setUser(user);
        console.log('Refresao si');
        console.log(user);
        return user;
      })
    );
  }

  activateAccount(activateAccount: IActivateAccount): Observable<boolean> {
    return this.http.post<boolean>(
      `${BASE_URL}/activate-account`,
      activateAccount
    );
  }

  setUser(user: IAccount | null) {
    this.user.next(user);
    if (user)
      this.cookieService.set('userToken', user.token, undefined, '/', environment.isProduction ? 'noshnexus.com' : 'localhost', environment.isProduction, 'Lax');
  }

  getToken() {
    return this.cookieService.get('userToken');
  }

  isLoggedIn(): boolean {
    return this.cookieService.check('userToken');
  }

  logout() {
    this.cookieService.delete('userToken', '/', environment.isProduction ? 'noshnexus.com' : 'localhost', environment.isProduction, 'Lax');
    this.setUser(null);
  }


  getAccountDetails(): Observable<IGetAccountDetails> {
    return this.http.get<IGetAccountDetails>(`${BASE_URL}/get-account-details`);
  }

  getAccountEdit(): Observable<IGetAccountEdit> {
    return this.http.get<IGetAccountEdit>(`${BASE_URL}/get-account-edit`);
  }

  editAccount(account: IEditAccount): Observable<IAccount> {
    return this.http.put<IAccount>(`${BASE_URL}/update-account`, account).pipe(
      map(user => {
        this.setUser(user);
        return user;
      })
    );
  }

  uploadProfileImage(image: FormData): Observable<IImageCard> {
    return this.http.post<IImageCard>(`${BASE_URL}/upload-profile-image`, image);
  }
}
