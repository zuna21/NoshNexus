import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IAccountLogin, IUser } from '../_interfaces/IAccount';
import { CookieService } from 'ngx-cookie-service';
import { IEditOwner, IGetOwner, IGetOwnerEdit } from '../_interfaces/IOwner';
import { IImageCard } from '../_interfaces/IImage';

const BASE_URL: string = `${environment.apiUrl}/account`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) {}

  getOwner(): Observable<IGetOwner> {
    return this.http.get<IGetOwner>(`http://localhost:5000/api/owner/owners/get-owner`);
  }

  getOwnerEdit(): Observable<IGetOwnerEdit> {
    return this.http.get<IGetOwnerEdit>(`http://localhost:5000/api/owner/owners/get-owner-edit`);
  }

  uploadProfileImage(image: FormData): Observable<IImageCard> {
    return this.http.post<IImageCard>(`http://localhost:5000/api/owner/owners/upload-profile-image`, image);
  }

  login(loginUser: IAccountLogin): Observable<IUser> {
    return this.http.post<IUser>(`http://localhost:5000/api/owner/account/login`, loginUser).pipe(
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
  }
}
