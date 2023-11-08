import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IGetRestaurantCreate, IRestaurantCard, IRestaurantDetails, IRestaurantEdit, IRestaurantSelect } from '../_interfaces/IRestaurant';

const BASE_URL: string = `${environment.apiUrl}/restaurant`;

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(
    private http: HttpClient
  ) { }

  getRestaurants(): Observable<IRestaurantCard[]> {
    return this.http.get<IRestaurantCard[]>(`http://localhost:5000/api/owner/restaurants/get-restaurants`);
  }

  getRestaurant(restaurantId: string): Observable<IRestaurantDetails> {
    return this.http.get<IRestaurantDetails>(`http://localhost:5000/api/owner/restaurants/get-restaurant/${restaurantId}`);
  }

  getOwnerRestaurantEdit(restaurantId: string): Observable<IRestaurantEdit> {
    return this.http.get<IRestaurantEdit>(`${BASE_URL}/get-owner-restaurant-edit/${restaurantId}`);
  }

  getOwnerRestaurantsForSelect(): Observable<IRestaurantSelect[]> {
    return this.http.get<IRestaurantSelect[]>(`${BASE_URL}/get-owner-restaurants-for-select`);
  }

  getRestaurantCreate(): Observable<IGetRestaurantCreate> {
    return this.http.get<IGetRestaurantCreate>(`http://localhost:5000/api/owner/restaurants/get-restaurant-create`);
  }
}
