import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IRestaurantCard } from '../components/restaurant-card/restaurant-card.component';

const BASE_URL: string = `${environment.apiUrl}/restaurants`

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(
    private http: HttpClient
  ) { }

  getRestaurants(): Observable<IRestaurantCard[]> {
    return this.http.get<IRestaurantCard[]>(`${BASE_URL}/get-restaurants`);
  }
}
