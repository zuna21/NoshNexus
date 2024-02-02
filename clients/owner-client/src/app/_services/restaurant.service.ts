import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  ICreateRestaurant,
  IEditRestaurant,
  IGetEditRestaurant,
  IGetRestaurantCreate,
  IRestaurantCard,
  IRestaurantDetails,
  IRestaurantSelect,
} from '../_interfaces/IRestaurant';
import { IChangeProfileImage, IImageCard } from '../_interfaces/IImage';
import { IRestaurantsQueryParams } from '../_interfaces/query_params.interface';
import { environment } from 'src/environments/environment';

const OWNER_URL: string = `${environment.apiUrl}/owner/restaurants`

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  constructor(private http: HttpClient) {}

  create(restaurant: ICreateRestaurant): Observable<IRestaurantSelect> {
    return this.http.post<IRestaurantSelect>(
      `${OWNER_URL}/create`,
      restaurant
    );
  }

  update(restaurantId: string, restaurant: IEditRestaurant) {
    return this.http.put(
      `${OWNER_URL}/update/${restaurantId}`,
      restaurant
    );
  }

  delete(restaurantId: number): Observable<number> {
    return this.http.delete<number>(`${OWNER_URL}/delete/${restaurantId}`);
  }

  uploadProfileImage(
    restaurantId: string,
    image: FormData
  ): Observable<IChangeProfileImage> {
    return this.http.post<IChangeProfileImage>(
      `${OWNER_URL}/upload-profile-image/${restaurantId}`,
      image
    );
  }

  uploadImages(
    restaurantId: string,
    images: FormData
  ): Observable<IImageCard[]> {
    return this.http.post<IImageCard[]>(
      `${OWNER_URL}/upload-images/${restaurantId}`,
      images
    );
  }

  deleteImage(restaurantId: string, imageId: string | number) {
    return this.http.delete(
      `${OWNER_URL}/delete-image/${restaurantId}/${imageId}`
    );
  }

  getRestaurants(restaurantsQueryParams: IRestaurantsQueryParams): Observable<IRestaurantCard[]> {
    let params = new HttpParams();
    if (restaurantsQueryParams.search) params = params.set('search', restaurantsQueryParams.search);

    return this.http.get<IRestaurantCard[]>(
      `${OWNER_URL}/get-restaurants`,
      { params }
    );
  }

  getRestaurant(restaurantId: number): Observable<IRestaurantDetails> {
    return this.http.get<IRestaurantDetails>(
      `${OWNER_URL}/get-restaurant/${restaurantId}`
    );
  }

  getRestaurantEdit(restaurantId: string): Observable<IGetEditRestaurant> {
    return this.http.get<IGetEditRestaurant>(
      `${OWNER_URL}/get-restaurant-edit/${restaurantId}`
    );
  }

  getOwnerRestaurantsForSelect(): Observable<IRestaurantSelect[]> {
    return this.http.get<IRestaurantSelect[]>(
      `${OWNER_URL}/get-restaurants-for-select`
    );
  }

  getRestaurantCreate(): Observable<IGetRestaurantCreate> {
    return this.http.get<IGetRestaurantCreate>(
      `${OWNER_URL}/get-restaurant-create`
    );
  }
}
