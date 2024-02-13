import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITable } from '../interfaces/table.interface';
import { environment } from '../../environments/environment';

const BASE_URL: string = `${environment.apiUrl}/tables`;

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(
    private http: HttpClient
  ) { }

  getTables(restaurantId: number): Observable<ITable[]> {
    return this.http.get<ITable[]>(`${BASE_URL}/get-tables/${restaurantId}`);
  }
}
