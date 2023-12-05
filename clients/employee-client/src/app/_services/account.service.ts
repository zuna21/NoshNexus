import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IAccountLogin, IUser } from '../_interfaces/IAccount';
import { CookieService } from 'ngx-cookie-service';
import { IEditOwner, IGetOwner, IGetOwnerEdit } from '../_interfaces/IOwner';
import { IImageCard } from '../_interfaces/IImage';
import { NotificationHubService } from './notification-hub.service';

const BASE_URL: string = `${environment.apiUrl}/account`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private user = new BehaviorSubject<IUser | null>(null);
  user$ = this.user.asObservable();

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private notificationHubService: NotificationHubService
  ) {}

  getUser(): Observable<IUser> {
    return this.http.get<IUser>(`http://localhost:5000/api/employee/account/get-user`).pipe(
      map(user => {
        this.setUser(user);
        return user;
      })
    );
  }

  getOwner(): Observable<IGetOwner> {
    return this.http.get<IGetOwner>(`http://localhost:5000/api/owner/owners/get-owner`);
  }

  getOwnerEdit(): Observable<IGetOwnerEdit> {
    return this.http.get<IGetOwnerEdit>(`http://localhost:5000/api/owner/owners/get-owner-edit`);
  }

  uploadProfileImage(image: FormData): Observable<IImageCard> {
    return this.http.post<IImageCard>(`http://localhost:5000/api/owner/account/upload-profile-image`, image);
  }

  login(loginUser: IAccountLogin): Observable<IUser> {
    return this.http.post<IUser>(`http://localhost:5000/api/employee/account/login`, loginUser).pipe(
      map((user: IUser) => {
        this.cookieService.set('userToken', user.token, undefined, '/', 'localhost', false, 'Lax');
        return user;
      })
    );
  }

  update(owner: IEditOwner): Observable<number> {
    return this.http.put<number>(`http://localhost:5000/api/owner/owners/update`, owner);
  }

  isLoggedIn(): boolean {
    return this.cookieService.check('userToken');
  }

  getToken(): string | null {
    return this.cookieService.get('userToken');
  }

  logout() {
    this.cookieService.delete('userToken', '/', 'localhost', false, 'Lax');
    this.setUser(null);
    this.notificationHubService.stopConnection();
  }



  setUser(user: IUser | null) {
    this.user.next(user);
    if (user) {
      this.cookieService.set('userToken', user.token, undefined, '/', 'localhost', false, 'Lax')
    };
    console.log(user);
  }
}
