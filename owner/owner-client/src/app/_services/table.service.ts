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

  getOwnerTables(): Observable<ITableCard[]> {
    return this.http.get<ITableCard[]>(`${BASE_URL}/get-owner-tables`);
  }
}
