import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGetAccountDetails } from '../_interfaces/account.interface';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

const EMPLOYEE_URL = `${environment.apiUrl}/employee/account`

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private http: HttpClient
  ) { }

  getAccountDetails(): Observable<IGetAccountDetails> {
    return this.http.get<IGetAccountDetails>(`${EMPLOYEE_URL}/get-account-details`);
  }
}
