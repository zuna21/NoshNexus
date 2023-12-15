import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ICustomer, ILoginCustomer, IRegisterCustomer } from '../_interfaces/ICustomer';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) { }

  login(user: ILoginCustomer): Observable<ICustomer> {
    return this.http.post<ICustomer>(`http://localhost:5000/api/account/login`, user).pipe(
      map(user => {
        this.cookieService.set('nosh_nexus_token', user.token, undefined, '/', 'localhost', false, 'Lax');
        localStorage.setItem('nosh_nexus_token', user.token);
        return user;
      })
    );
  }

  loginAsGuest(): Observable<ICustomer> {
    return this.http.get<ICustomer>(`http://localhost:5000/api/account/login-as-guest`).pipe(
      map(user => {
        this.cookieService.set('nosh_nexus_token', user.token, undefined, '/', 'localhost', false, 'Lax');
        localStorage.setItem('nosh_nexus_token', user.token);
        return user;
      })
    );
  }

  register(customer: IRegisterCustomer): Observable<ICustomer> {
    return this.http.post<ICustomer>(`http://localhost:5000/api/account/register`, customer).pipe(
      map(user => {
        this.cookieService.set('nosh_nexus_token', user.token, undefined, '/', 'localhost', false, 'Lax');
        localStorage.setItem('nosh_nexus_token', user.token);
        return user;
      })
    );
  }

  isLoggedIn(): boolean {
    return this.cookieService.check('nosh_nexus_token');
  }

  getToken(): string {
    return this.cookieService.get('nosh_nexus_token');
  }

  logout() {
    this.cookieService.delete('nosh_nexus_token', '/', 'localhost', false, 'Lax');
    localStorage.removeItem('nosh_nexus_token');
  }
}
