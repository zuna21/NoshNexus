import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRestaurantCard, IRestaurantDetails } from '../_interfaces/IRestaurant';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(
    private http: HttpClient
  ) { }

  getRestaurants(sq: string = ''): Observable<IRestaurantCard[]> {
    return this.http.get<IRestaurantCard[]>(`http://localhost:5000/api/restaurants/get-restaurants?sq=${sq}`);
  }

  getRestaurant(restaurantId: number): Observable<IRestaurantDetails> {
    return this.http.get<IRestaurantDetails>(`http://localhost:5000/api/restaurants/get-restaurant/${restaurantId}`);
  }
}
