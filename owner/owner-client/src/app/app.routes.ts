import { Routes } from '@angular/router';
import { MainComponentsComponent } from './_components/main-components/main-components.component';
import { HomeComponent } from './_layouts/home/home.component';
import { RestaurantsComponent } from './_layouts/restaurants/restaurants.component';
import { RestaurantsCreateComponent } from './_layouts/restaurants/restaurants-create/restaurants-create.component';
import { RestaurantsDetailsComponent } from './_layouts/restaurants/restaurants-details/restaurants-details.component';
import { RestaurantsEditComponent } from './_layouts/restaurants/restaurants-edit/restaurants-edit.component';

export const routes: Routes = [
    {
        path: '', component: MainComponentsComponent, children: [
          { path: '', pathMatch: 'full', redirectTo: '/home' },
          { path: 'home', component: HomeComponent },
    
          { path: 'restaurants', component: RestaurantsComponent },
          { path: 'restaurants/create', component: RestaurantsCreateComponent},
          { path: 'restaurants/:id', component: RestaurantsDetailsComponent},
          { path: 'restaurants/edit/:id', component: RestaurantsEditComponent}
        ]
      }
];
