import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRestaurantCard } from '../_interfaces/IRestaurant';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(
    private http: HttpClient
  ) { }

  getRestaurants(): Observable<IRestaurantCard[]> {
    return this.http.get<IRestaurantCard[]>(`http://localhost:3000/restaurants/get-restaurants`)
  }
}
