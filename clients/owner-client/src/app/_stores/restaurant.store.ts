import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IRestaurantCard } from '../_interfaces/IRestaurant';

@Injectable({
  providedIn: 'root',
})
export class RestaurantStore {
    private restaurants = new BehaviorSubject<IRestaurantCard[]>([]);
    restaurants$ = this.restaurants.asObservable();

    getRestaurants(): IRestaurantCard[] {
        return this.restaurants.getValue();
    }

    setRestaurants(restaurants: IRestaurantCard[]) {
        this.restaurants.next([...restaurants]);
    }

}
