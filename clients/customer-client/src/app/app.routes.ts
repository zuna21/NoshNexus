import { Routes } from '@angular/router';
import { HomeComponent } from './layouts/home/home.component';
import { MainComponent } from './layouts/main/main.component';
import { RestaurantDetailsComponent } from './layouts/restaurant/restaurant-details/restaurant-details.component';
import { SelectionComponent } from './layouts/selection/selection.component';
import { MenuDetailsComponent } from './layouts/menus/menu-details/menu-details.component';
import { OrderPreviewComponent } from './layouts/order-preview/order-preview.component';
import { EmployeesComponent } from './layouts/employees/employees.component';
import { EmployeeDetailsComponent } from './layouts/employees/employee-details/employee-details.component';
import { OrdersComponent } from './layouts/orders/orders.component';
import { AccountComponent } from './layouts/account/account.component';
import { EditAccountComponent } from './layouts/account/edit-account/edit-account.component';

export const routes: Routes = [
    { path: '', component: MainComponent, children: [
        { path: '', pathMatch: 'full', redirectTo: '/home'},
        { path: 'home', component: HomeComponent },

        { path: 'account', component: AccountComponent },
        { path: 'edit-account', component: EditAccountComponent },

        { path: 'restaurants/:restaurantId', component: RestaurantDetailsComponent },
        { path: 'restaurants/:restaurantId/employees', component: EmployeesComponent },

        { path: 'selection/:restaurantId', component: SelectionComponent },

        { path: 'selection/:restaurantId/:menuId', component: MenuDetailsComponent },

        { path: 'order-preview', component: OrderPreviewComponent },

        { path: 'employees/:employeeId', component: EmployeeDetailsComponent },

        { path: 'orders', component: OrdersComponent }
    ] }
];
