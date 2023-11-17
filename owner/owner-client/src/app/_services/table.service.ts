import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ITableCard } from '../_interfaces/ITable';

const BASE_URL: string = `${environment.apiUrl}/table`;

@Injectable({
  providedIn: 'root',
})
export class TableService {
  constructor(private http: HttpClient) {}

  create(restaurantTables: ITableCard[]): Observable<boolean> {
    return this.http.post<boolean>(`http://localhost:5000/api/owner/tables/create`, restaurantTables);
  }

  delete(tableId: number): Observable<boolean> {
    return this.http.delete<boolean>(`http://localhost:5000/api/owner/tables/delete/${tableId}`);
  }

  getOwnerTables(): Observable<ITableCard[]> {
    return this.http.get<ITableCard[]>(`http://localhost:5000/api/owner/tables/get-tables`);
  }
}
