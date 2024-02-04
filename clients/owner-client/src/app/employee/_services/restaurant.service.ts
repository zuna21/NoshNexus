import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRestaurantDetails } from 'src/app/_interfaces/IRestaurant';
import { environment } from 'src/environments/environment';

const EMPLOYEE_URL: string = `${environment.apiUrl}/employee/restaurants`

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(
    private http: HttpClient
  ) { }

  getRestaurant(): Observable<IRestaurantDetails> {
    return this.http.get<IRestaurantDetails>(`${EMPLOYEE_URL}/get-restaurant`);
  }
}
