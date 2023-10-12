import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IEmployeeCard, IEmployeeDetails, IEmployeeEdit } from '../_interfaces/IEmployee';

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

  getOwnerEmployeeDetails(employeeId: string): Observable<IEmployeeDetails> {
    return this.http.get<IEmployeeDetails>(`${BASE_URL}/get-owner-employee-details/${employeeId}`);
  }

  getOwnerEmployeeEdit(employeeId: string): Observable<IEmployeeEdit> {
    return this.http.get<IEmployeeEdit>(`${BASE_URL}/get-owner-employee-edit/${employeeId}`);
  }
}
