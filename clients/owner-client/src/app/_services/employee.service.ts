import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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
import { environment } from 'src/environments/environment';

const OWNER_URL: string = `${environment.apiUrl}/owner/employees`;

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  constructor(private http: HttpClient) {}

  create(employee: ICreateEmployee): Observable<number> {
    return this.http.post<number>(
      `${OWNER_URL}/create`,
      employee
    );
  }

  update(employeeId: string, employee: IEditEmployee): Observable<number> {
    return this.http.put<number>(
      `${OWNER_URL}/update/${employeeId}`,
      employee
    );
  }

  delete(employeeId: number): Observable<number> {
    return this.http.delete<number>(
      `${OWNER_URL}/delete/${employeeId}`
    );
  }

  uploadProfileImage(
    employeeId: string,
    image: FormData
  ): Observable<IImageCard> {
    return this.http.post<IImageCard>(
      `${OWNER_URL}/upload-profile-image/${employeeId}`,
      image
    );
  }

  deleteImage(employeeId: string, imageId: number | string) : Observable<number> {
    return this.http.delete<number>(`${OWNER_URL}/delete-image/${employeeId}/${imageId}`);
  }

  getEmployees(
    employeesQueryParams: IEmployeesQueryParams
  ): Observable<IPagedList<IEmployeeCard[]>> {
    let params = new HttpParams();
    params = params.set('pageIndex', employeesQueryParams.pageIndex);
    if (employeesQueryParams.search) params = params.set('search', employeesQueryParams.search);
    if (employeesQueryParams.restaurant) params = params.set('restaurant', employeesQueryParams.restaurant);
    return this.http.get<IPagedList<IEmployeeCard[]>>(
      `${OWNER_URL}/get-employees`,
      { params }
    );
  }

  getEmployee(employeeId: string): Observable<IEmployeeDetails> {
    return this.http.get<IEmployeeDetails>(
      `${OWNER_URL}/get-employee/${employeeId}`
    );
  }

  getEmployeeEdit(employeeId: string): Observable<IGetEditEmployee> {
    return this.http.get<IGetEditEmployee>(
      `${OWNER_URL}/get-employee-edit/${employeeId}`
    );
  }
}
