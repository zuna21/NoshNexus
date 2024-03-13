import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { IAccountLogin, IRefreshToken, IUser } from '../_interfaces/IAccount';
import { IEditOwner, IGetOwner, IGetOwnerEdit } from '../_interfaces/IOwner';
import { IImageCard } from '../_interfaces/IImage';
import { environment } from 'src/environments/environment';
import { jwtDecode } from 'jwt-decode';
import { IEditAccount, IGetAccountDetails, IGetAccountEdit } from '../employee/_interfaces/account.interface';
import { Router } from '@angular/router';

const OWNER_URL: string = `${environment.apiUrl}/owner`;
const USER_URL: string = `${environment.apiUrl}/user`;
const EMPLOYEE_URL: string = `${environment.apiUrl}/employee`;

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private user = new BehaviorSubject<IUser | null>(null);
  user$ = this.user.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  refreshUser(): Observable<IUser> {
    return this.http.get<IUser>(`${USER_URL}/account/refresh-user`).pipe(
      map(user => {
        this.setUser(user);
        return user;
      })
    );
  }

  getOwner(): Observable<IGetOwner> {
    return this.http.get<IGetOwner>(`${OWNER_URL}/owners/get-owner`);
  }

  getEmployee(): Observable<IGetAccountDetails> {
    return this.http.get<IGetAccountDetails>(`${EMPLOYEE_URL}/account/get-account-details`);
  }

  getEmployeeEdit(): Observable<IGetAccountEdit> {
    return this.http.get<IGetAccountEdit>(`${EMPLOYEE_URL}/account/get-account-edit`);
  }

  updateEmployee(account: IEditAccount): Observable<IUser> {
    return this.http.put<IUser>(`${EMPLOYEE_URL}/account/edit-account`, account).pipe(
      map(user => {
        this.setUser(user);
        return user;
      })
    );
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
    return this.http.post<IUser>(`${USER_URL}/account/login`, loginUser).pipe(
      map((user: IUser) => {
        this.setUser(user);
        return user;
      })
    );
  }

  update(owner: IEditOwner): Observable<IUser> {
    return this.http.put<IUser>(`${OWNER_URL}/owners/update`, owner).pipe(
      map((user: IUser) => {
        this.setUser(user);
        return user;
      })
    );
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('token') !== null;
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem("refreshToken");
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    return JSON.parse(JSON.stringify(jwtDecode(token))).role;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    this.setUser(null);
    this.router.navigateByUrl('/login');
  }



  setUser(user: IUser | null) {
    this.user.next(user);
    if (user) {
      localStorage.setItem('token', user.token);
      localStorage.setItem('refreshToken', user.refreshToken);
    };
    console.log(user);
  }

  getUserSubject(): IUser | null {
    return this.user.getValue();
  }








  test() {
    return this.http.get(`${USER_URL}/account/test`);
  }

  refreshToken() {
    const token = this.getToken();
    const refreshToken = this.getRefreshToken();
    if (!token || !refreshToken) return;
    const refreshTokenReq: IRefreshToken = {
      token: token,
      refreshToken: refreshToken
    };
    return this.http.post<IUser>(`${USER_URL}/account/refresh-token`, refreshTokenReq).pipe(
      map((response) => {
        if (response) this.setUser(response);
        return response;
      })
    );
  }
}
