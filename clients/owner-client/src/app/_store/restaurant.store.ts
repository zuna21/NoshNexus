import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IRestaurantSelect } from '../_interfaces/IRestaurant';

@Injectable({
  providedIn: 'root',
})
export class RestaurantStore {
    private restaurantsForSelect = new BehaviorSubject<IRestaurantSelect[]>([]);
    restaurantsForSelect$ = this.restaurantsForSelect.asObservable();

    setRestaurantsForSelect(restaurants: IRestaurantSelect[]) {
        this.restaurantsForSelect.next(restaurants);
    }

    addRestaurantForSelect(restaurant: IRestaurantSelect) {
        const oldRestaurants = this.restaurantsForSelect.getValue();
        const newRestaurants = [...oldRestaurants, restaurant];
        this.restaurantsForSelect.next(newRestaurants);
    }

    removeRestaurantForSelect(restaurantId: number) {
        const oldRestaurants = this.restaurantsForSelect.getValue();
        const newRestaurants = oldRestaurants.filter(x => x.id !== restaurantId);
        this.restaurantsForSelect.next(newRestaurants);
    }

    getRestaurantsForSelect(): IRestaurantSelect[] {
        return this.restaurantsForSelect.getValue();
    }

}
