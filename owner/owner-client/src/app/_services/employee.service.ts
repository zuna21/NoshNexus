import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IEmployeeCard } from '../_interfaces/IEmployee';

const BASE_URL: string = `${environment.apiUrl}/employee`;

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(
    private http: HttpClient
  ) { }

  getOwnerEmployeesCards(): Observable<IEmployeeCard[]> {
    return this.http.get<IEmployeeCard[]>(`${BASE_URL}/get-owner-employees-cards`);
  }
}
