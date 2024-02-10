import { Routes } from '@angular/router';
import { HomeComponent } from './layouts/home/home.component';
import { MainComponent } from './layouts/main/main.component';
import { RestaurantDetailsComponent } from './layouts/restaurant/restaurant-details/restaurant-details.component';
import { SelectionComponent } from './layouts/selection/selection.component';

export const routes: Routes = [
    { path: '', component: MainComponent, children: [
        { path: '', pathMatch: 'full', redirectTo: '/home'},
        { path: 'home', component: HomeComponent },

        { path: 'restaurants/:restaurantId', component: RestaurantDetailsComponent },

        { path: 'selection/:restaurantId', component: SelectionComponent }
    ] }
];
