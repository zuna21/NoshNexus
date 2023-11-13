import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import {
  IGetRestaurantCreate,
  IRestaurantCard,
  IRestaurantDetails,
  IRestaurantEdit,
  IRestaurantSelect,
} from '../_interfaces/IRestaurant';
import { IChangeProfileImage, IImageCard } from '../_interfaces/IImage';

const BASE_URL: string = `${environment.apiUrl}/restaurant`;

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  constructor(private http: HttpClient) {}

  create(restaurant: IRestaurantCard): Observable<number> {
    return this.http.post<number>(
      `http://localhost:5000/api/owner/restaurants/create`,
      restaurant
    );
  }

  uploadProfileImage(
    restaurantId: string,
    image: FormData
  ): Observable<IChangeProfileImage> {
    return this.http.post<IChangeProfileImage>(
      `http://localhost:5000/api/owner/restaurants/upload-profile-image/${restaurantId}`,
      image
    );
  }

  uploadImages(
    restaurantId: string,
    images: FormData
  ): Observable<IImageCard[]> {
    return this.http.post<IImageCard[]>(
      `http://localhost:5000/api/owner/restaurants/upload-images/${restaurantId}`,
      images
    );
  }

  deleteImage(restaurantId: string, imageId: string | number) {
    return this.http.delete(
      `http://localhost:5000/api/owner/restaurants/delete-image/${restaurantId}/${imageId}`
    );
  }

  getRestaurants(): Observable<IRestaurantCard[]> {
    return this.http.get<IRestaurantCard[]>(
      `http://localhost:5000/api/owner/restaurants/get-restaurants`
    );
  }

  getRestaurant(restaurantId: string): Observable<IRestaurantDetails> {
    return this.http.get<IRestaurantDetails>(
      `http://localhost:5000/api/owner/restaurants/get-restaurant/${restaurantId}`
    );
  }

  getRestaurantEdit(restaurantId: string): Observable<IRestaurantEdit> {
    return this.http.get<IRestaurantEdit>(
      `http://localhost:5000/api/owner/restaurants/get-restaurant-edit/${restaurantId}`
    );
  }

  getOwnerRestaurantsForSelect(): Observable<IRestaurantSelect[]> {
    return this.http.get<IRestaurantSelect[]>(
      `${BASE_URL}/get-owner-restaurants-for-select`
    );
  }

  getRestaurantCreate(): Observable<IGetRestaurantCreate> {
    return this.http.get<IGetRestaurantCreate>(
      `http://localhost:5000/api/owner/restaurants/get-restaurant-create`
    );
  }
}
