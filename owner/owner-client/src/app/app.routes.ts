import { Routes } from '@angular/router';
import { MainComponentsComponent } from './_components/main-components/main-components.component';
import { HomeComponent } from './_layouts/home/home.component';
import { RestaurantsComponent } from './_layouts/restaurants/restaurants.component';
import { RestaurantsCreateComponent } from './_layouts/restaurants/restaurants-create/restaurants-create.component';
import { RestaurantsDetailsComponent } from './_layouts/restaurants/restaurants-details/restaurants-details.component';
import { RestaurantsEditComponent } from './_layouts/restaurants/restaurants-edit/restaurants-edit.component';
import { EmployeesComponent } from './_layouts/employees/employees.component';
import { EmployeesCreateComponent } from './_layouts/employees/employees-create/employees-create.component';
import { EmployeesDetailsComponent } from './_layouts/employees/employees-details/employees-details.component';
import { EmployeesEditComponent } from './_layouts/employees/employees-edit/employees-edit.component';

export const routes: Routes = [
  {
    path: '',
    component: MainComponentsComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: '/home' },
      { path: 'home', component: HomeComponent },

      { path: 'restaurants', component: RestaurantsComponent },
      { path: 'restaurants/create', component: RestaurantsCreateComponent },
      { path: 'restaurants/:id', component: RestaurantsDetailsComponent },
      { path: 'restaurants/edit/:id', component: RestaurantsEditComponent },

      { path: 'employees', component: EmployeesComponent },
      { path: 'employees/create', component: EmployeesCreateComponent },
      { path: 'employees/:id', component: EmployeesDetailsComponent },
      { path: 'employees/edit/:id', component: EmployeesEditComponent },
    ],
  },
];
