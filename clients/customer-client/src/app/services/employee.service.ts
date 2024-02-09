import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IEmployee, IEmployeeCard } from '../interfaces/employee.interface';

const BASE_URL: string = `${environment.apiUrl}/employees`;

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(
    private http: HttpClient
  ) { }

  getEmployees(restaurantId: number): Observable<IEmployeeCard[]> {
    return this.http.get<IEmployeeCard[]>(`${BASE_URL}/get-employees/${restaurantId}`);
  }

  getEmployee(employeeId: number): Observable<IEmployee> {
    return this.http.get<IEmployee>(`${BASE_URL}/get-employee/${employeeId}`);
  }
}
