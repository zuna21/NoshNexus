import { Routes } from '@angular/router';
import { MainLayoutComponent } from './_layouts/main-layout/main-layout.component';
import { HomeComponent } from './_layouts/home/home.component';
import { RestaurantsComponent } from './_layouts/restaurants/restaurants.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', pathMatch: 'full', redirectTo: '/restaurants' },

            { path: 'home', component: HomeComponent },

            { path: 'restaurants', component: RestaurantsComponent }
        ]
    }
];
