import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { IAccountLogin, IUser } from '../_interfaces/IAccount';
import { CookieService } from 'ngx-cookie-service';
import { IEditOwner, IGetOwner, IGetOwnerEdit } from '../_interfaces/IOwner';
import { IImageCard } from '../_interfaces/IImage';
import { environment } from 'src/environments/environment';

const OWNER_URL: string = `${environment.apiUrl}/owner`;
const EMPLOYEE_URL: string = `${environment.apiUrl}/employee`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private user = new BehaviorSubject<IUser | null>(null);
  user$ = this.user.asObservable();

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) { }

  getUser(): Observable<IUser> {
    return this.http.get<IUser>(`${EMPLOYEE_URL}/account/get-user`).pipe(
      map(user => {
        this.setUser(user);
        return user;
      })
    );
  }

  getOwner(): Observable<IGetOwner> {
    return this.http.get<IGetOwner>(`${OWNER_URL}/owners/get-owner`);
  }

  getOwnerEdit(): Observable<IGetOwnerEdit> {
    return this.http.get<IGetOwnerEdit>(`${OWNER_URL}/owners/get-owner-edit`);
  }

  uploadProfileImage(image: FormData): Observable<IImageCard> {
    return this.http.post<IImageCard>(`${OWNER_URL}/account/upload-profile-image`, image);
  }

  deleteImage(imageId: string | number): Observable<number> {
    return this.http.delete<number>(`${OWNER_URL}/account/delete-image/${imageId}`);
  }

  login(loginUser: IAccountLogin): Observable<IUser> {
    return this.http.post<IUser>(`${OWNER_URL}/account/login`, loginUser).pipe(
      map((user: IUser) => {
        this.cookieService.set('userToken', user.token, undefined, '/', environment.production ? 'noshnexus.com' : 'localhost', environment.production, 'Lax');
        return user;
      })
    );
  }

  update(owner: IEditOwner): Observable<number> {
    return this.http.put<number>(`${OWNER_URL}/owners/update`, owner);
  }

  isLoggedIn(): boolean {
    return this.cookieService.check('userToken');
  }

  getToken(): string | null {
    return this.cookieService.get('userToken');
  }

  logout() {
    this.cookieService.delete('userToken', '/', environment.production ? 'noshnexus.com' : 'localhost', environment.production, 'Lax');
    this.setUser(null);
  }



  setUser(user: IUser | null) {
    this.user.next(user);
    if (user) {
      this.cookieService.set('userToken', user.token, undefined, '/', environment.production ? 'noshnexus.com' : 'localhost', environment.production, 'Lax')
    };
    console.log(user);
  }
}
