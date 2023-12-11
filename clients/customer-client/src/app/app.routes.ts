import { Routes } from '@angular/router';
import { MainLayoutComponent } from './_layouts/main-layout/main-layout.component';
import { HomeComponent } from './_layouts/home/home.component';
import { RestaurantsComponent } from './_layouts/restaurants/restaurants.component';
import { RestaurantDetailsComponent } from './_layouts/restaurants/restaurant-details/restaurant-details.component';
import { MakeOrderComponent } from './_layouts/make-order/make-order.component';
import { MenuDetailsComponent } from './_layouts/make-order/menus/menu-details/menu-details.component';
import { OrderDialogComponent } from './_layouts/make-order/order-dialog/order-dialog.component';
import { LoginComponent } from './_layouts/login/login.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', pathMatch: 'full', redirectTo: '/restaurants' },

            { path: 'home', component: HomeComponent },

            { path: 'restaurants', component: RestaurantsComponent },
            { path: 'restaurants/:restaurantId', component: RestaurantDetailsComponent },
            { path: 'restaurants/:restaurantId/make-order', component: MakeOrderComponent },
            { path: 'restaurants/:restaurantId/make-order/menus/:menuId', component: MenuDetailsComponent },

            { path: 'order-dialog', component: OrderDialogComponent }

        ]
    },
    { path: 'login', component: LoginComponent }
];
