import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITable } from '../_interfaces/ITable';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(
    private http: HttpClient
  ) { }

  getTables(restaurantId: number): Observable<ITable[]> {
    return this.http.get<ITable[]>(`http://localhost:5000/api/tables/get-tables/${restaurantId}`);
  }
}
