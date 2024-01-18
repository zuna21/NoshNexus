import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import {
  ICreateEmployee,
  IEditEmployee,
  IEmployeeCard,
  IEmployeeDetails,
  IGetEditEmployee,
} from '../_interfaces/IEmployee';
import { IImageCard } from '../_interfaces/IImage';
import { IPagedList } from '../_interfaces/IPagedList';
import { IEmployeesQueryParams } from '../_interfaces/query_params.interface';

const BASE_URL: string = `${environment.apiUrl}/employee`;

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  constructor(private http: HttpClient) {}

  create(employee: ICreateEmployee): Observable<number> {
    return this.http.post<number>(
      `http://localhost:5000/api/owner/employees/create`,
      employee
    );
  }

  update(employeeId: string, employee: IEditEmployee): Observable<number> {
    return this.http.put<number>(
      `http://localhost:5000/api/owner/employees/update/${employeeId}`,
      employee
    );
  }

  delete(employeeId: number): Observable<number> {
    return this.http.delete<number>(
      `http://localhost:5000/api/owner/employees/delete/${employeeId}`
    );
  }

  uploadProfileImage(
    employeeId: string,
    image: FormData
  ): Observable<IImageCard> {
    return this.http.post<IImageCard>(
      `http://localhost:5000/api/owner/employees/upload-profile-image/${employeeId}`,
      image
    );
  }

  getEmployees(
    employeesQueryParams: IEmployeesQueryParams
  ): Observable<IPagedList<IEmployeeCard[]>> {
    let params = new HttpParams();
    params = params.set('pageIndex', employeesQueryParams.pageIndex);
    if (employeesQueryParams.search) params = params.set('search', employeesQueryParams.search);
    if (employeesQueryParams.restaurant) params = params.set('restaurant', employeesQueryParams.restaurant);
    return this.http.get<IPagedList<IEmployeeCard[]>>(
      `http://localhost:5000/api/owner/employees/get-employees`,
      { params }
    );
  }

  getEmployee(employeeId: string): Observable<IEmployeeDetails> {
    return this.http.get<IEmployeeDetails>(
      `http://localhost:5000/api/owner/employees/get-employee/${employeeId}`
    );
  }

  getEmployeeEdit(employeeId: string): Observable<IGetEditEmployee> {
    return this.http.get<IGetEditEmployee>(
      `http://localhost:5000/api/owner/employees/get-employee-edit/${employeeId}`
    );
  }
}
