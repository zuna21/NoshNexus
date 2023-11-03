import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IAccount, IAccountEdit, IAccountLogin, IUser } from '../_interfaces/IAccount';
import { CookieService } from 'ngx-cookie-service';

const BASE_URL: string = `${environment.apiUrl}/account`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) {}

  getOwner(): Observable<IAccount> {
    return this.http.get<IAccount>(`${BASE_URL}/get-owner`);
  }

  getOwnerEdit(): Observable<IAccountEdit> {
    return this.http.get<IAccountEdit>(`${BASE_URL}/get-owner-edit`);
  }



  login(loginUser: IAccountLogin): Observable<IUser> {
    return this.http.post<IUser>(`http://localhost:5000/api/owner/account/login`, loginUser).pipe(
      map((user: IUser) => {
        this.cookieService.set('userToken', user.token, undefined, '/', 'localhost', false, 'Lax');
        return user;
      })
    );
  }

  logout() {
    this.cookieService.delete('userToken', '/', 'localhost', false, 'Lax');
  }
}
