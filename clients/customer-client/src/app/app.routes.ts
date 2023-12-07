import { Routes } from '@angular/router';
import { MainLayoutComponent } from './_layouts/main-layout/main-layout.component';
import { HomeComponent } from './_layouts/home/home.component';
import { RestaurantsComponent } from './_layouts/restaurants/restaurants.component';
import { RestaurantDetailsComponent } from './_layouts/restaurants/restaurant-details/restaurant-details.component';
import { MakeOrderComponent } from './_layouts/make-order/make-order.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', pathMatch: 'full', redirectTo: '/restaurants' },

            { path: 'home', component: HomeComponent },

            { path: 'restaurants', component: RestaurantsComponent },
            { path: 'restaurants/:id', component: RestaurantDetailsComponent },
            { path: 'restaurants/:id/make-order', component: MakeOrderComponent }

        ]
    }
];
