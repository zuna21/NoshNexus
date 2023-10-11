import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IRestaurantCard, IRestaurantDetails, IRestaurantEdit } from '../_interfaces/IRestaurant';

const BASE_URL: string = `${environment.apiUrl}/restaurant`;

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(
    private http: HttpClient
  ) { }

  getOwnerRestaurants(): Observable<IRestaurantCard[]> {
    return this.http.get<IRestaurantCard[]>(`${BASE_URL}/get-owner-restaurants`);
  }

  getOwnerRestaurantDetails(restaurantId: string): Observable<IRestaurantDetails> {
    return this.http.get<IRestaurantDetails>(`${BASE_URL}/get-owner-restaurant-details/${restaurantId}`);
  }

  getOwnerRestaurantEdit(restaurantId: string): Observable<IRestaurantEdit> {
    return this.http.get<IRestaurantEdit>(`${BASE_URL}/get-owner-restaurant-edit/${restaurantId}`);
  }
}
