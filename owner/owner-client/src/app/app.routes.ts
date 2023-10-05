import { Routes } from '@angular/router';
import { MainComponentsComponent } from './_components/main-components/main-components.component';
import { HomeComponent } from './_layouts/home/home.component';
import { RestaurantsComponent } from './_layouts/restaurants/restaurants.component';
import { RestaurantsCreateComponent } from './_layouts/restaurants/restaurants-create/restaurants-create.component';

export const routes: Routes = [
    {
        path: '', component: MainComponentsComponent, children: [
          { path: '', pathMatch: 'full', redirectTo: '/home' },
          { path: 'home', component: HomeComponent },
    
          { path: 'restaurants', component: RestaurantsComponent },
          { path: 'restaurants/create', component: RestaurantsCreateComponent}
        ]
      }
];
