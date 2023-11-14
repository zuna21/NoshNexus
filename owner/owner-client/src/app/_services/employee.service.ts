import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ICreateEmployee, IEditEmployee, IEmployeeCard, IEmployeeDetails, IGetEditEmployee } from '../_interfaces/IEmployee';

const BASE_URL: string = `${environment.apiUrl}/employee`;

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(
    private http: HttpClient
  ) { }

  create(employee: ICreateEmployee): Observable<number> {
    return this.http.post<number>(`http://localhost:5000/api/owner/employees/create`, employee);
  }

  update(employeeId: string, employee: IEditEmployee): Observable<number> {
    return this.http.put<number>(`http://localhost:5000/api/owner/employees/update/${employeeId}`, employee);
  }

  getEmployees(): Observable<IEmployeeCard[]> {
    return this.http.get<IEmployeeCard[]>(`http://localhost:5000/api/owner/employees/get-employees`);
  }

  getEmployee(employeeId: string): Observable<IEmployeeDetails> {
    return this.http.get<IEmployeeDetails>(`http://localhost:5000/api/owner/employees/get-employee/${employeeId}`);
  }

  getEmployeeEdit(employeeId: string): Observable<IGetEditEmployee> {
    return this.http.get<IGetEditEmployee>(`http://localhost:5000/api/owner/employees/get-employee-edit/${employeeId}`);
  }
}
