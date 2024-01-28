import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOrdersByDayParams, ITopTenMenuItemsParams } from '../_interfaces/query_params.interface';
import { IPieChart } from '../_interfaces/IChart';
import { environment } from 'src/environments/environment';

const OWNER_URL: string = `${environment.apiUrl}/owner`;

@Injectable({
  providedIn: 'root',
})
export class ChartService {
  constructor(private http: HttpClient) {}

  getOrdersByDay(
    restaurantId: number,
    ordersByDayQueryParams: IOrdersByDayParams
  ): Observable<number[]> {
    let params = new HttpParams();
    params = params.set('startDate', ordersByDayQueryParams.startDate);
    params = params.set('endDate', ordersByDayQueryParams.endDate);
    params = params.set('status', ordersByDayQueryParams.status);

    return this.http.get<number[]>(
      `${OWNER_URL}/get-orders-by-day/${restaurantId}`,
      { params }
    );
  }

  getTopTenMenuItems(restaurantId: number, topTenMenuItemsParams: ITopTenMenuItemsParams): Observable<IPieChart> {
    let params = new HttpParams();

    if (topTenMenuItemsParams.menu) params = params.set('menu', topTenMenuItemsParams.menu);

    return this.http.get<IPieChart>(
      `${OWNER_URL}/get-top-ten-menu-items/${restaurantId}`, { params }
    );
  }
}
