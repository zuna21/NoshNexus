import { Routes } from '@angular/router';
import { LoginComponent } from './_layouts/login/login.component';
import { authGuard } from './_guards/auth.guard';
import { anonGuard } from './_guards/anon.guard';
import { ownerGuard } from './_guards/owner.guard';
import { employeeRoutes } from './employee.routes';
import { HomeComponent } from './_layouts/home/home.component';
import { AccountComponent } from './_layouts/account/account.component';
import { AccountEditComponent } from './_layouts/account/account-edit/account-edit.component';
import { OrdersByDayComponent } from './_layouts/charts/orders-by-day/orders-by-day.component';
import { TopTenMenuItemsComponent } from './_layouts/charts/top-ten-menu-items/top-ten-menu-items.component';
import { OrdersByHourComponent } from './_layouts/charts/orders-by-hour/orders-by-hour.component';
import { RestaurantsComponent } from './_layouts/restaurants/restaurants.component';
import { RestaurantsCreateComponent } from './_layouts/restaurants/restaurants-create/restaurants-create.component';
import { RestaurantsDetailsComponent } from './_layouts/restaurants/restaurants-details/restaurants-details.component';
import { RestaurantsEditComponent } from './_layouts/restaurants/restaurants-edit/restaurants-edit.component';
import { EmployeesComponent } from './_layouts/employees/employees.component';
import { EmployeesCreateComponent } from './_layouts/employees/employees-create/employees-create.component';
import { EmployeesDetailsComponent } from './_layouts/employees/employees-details/employees-details.component';
import { EmployeesEditComponent } from './_layouts/employees/employees-edit/employees-edit.component';
import { MenusComponent } from './_layouts/menus/menus.component';
import { MenusCreateComponent } from './_layouts/menus/menus-create/menus-create.component';
import { MenusDetailsComponent } from './_layouts/menus/menus-details/menus-details.component';
import { MenusEditComponent } from './_layouts/menus/menus-edit/menus-edit.component';
import { MenuItemDetailsComponent } from './_layouts/menus/menu-item-details/menu-item-details.component';
import { MenuItemEditComponent } from './_layouts/menus/menu-item-edit/menu-item-edit.component';
import { TablesComponent } from './_layouts/tables/tables.component';
import { TablesCreateComponent } from './_layouts/tables/tables-create/tables-create.component';
import { TablesQrCodeComponent } from './_layouts/tables/tables-qr-code/tables-qr-code.component';
import { OrdersComponent } from './_layouts/orders/orders.component';
import { OrdersHistoryComponent } from './_layouts/orders/orders-history/orders-history.component';
import { ChatsComponent } from './_layouts/chats/chats.component';
import { NotificationsComponent } from './_layouts/notifications/notifications.component';
import { SettingsComponent } from './_layouts/settings/settings.component';
import { BlockedUsersComponent } from './_layouts/settings/blocked-users/blocked-users.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./_components/main-components/main-components.component').then(
        (mod) => mod.MainComponentsComponent
      ),
    canActivate: [authGuard, ownerGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: '/home' },
      {
        path: 'home',
        component: HomeComponent,
      },

      {
        path: 'account',
        component: AccountComponent,
      },
      {
        path: 'account/edit',
        component: AccountEditComponent,
      },

      {
        path: 'charts/orders-by-day/:restaurantId',
        component: OrdersByDayComponent,
      },

      {
        path: 'charts/top-ten-menu-items/:restaurantId',
        component: TopTenMenuItemsComponent,
      },

      {
        path: 'charts/orders-by-hour/:restaurantId',
        component: OrdersByHourComponent,
      },

      {
        path: 'restaurants',
        component: RestaurantsComponent,
      },
      {
        path: 'restaurants/create',
        component: RestaurantsCreateComponent,
      },
      {
        path: 'restaurants/:id',
        component: RestaurantsDetailsComponent,
      },
      {
        path: 'restaurants/edit/:id',
        component: RestaurantsEditComponent,
      },

      {
        path: 'employees',
        component: EmployeesComponent,
      },
      {
        path: 'employees/create',
        component: EmployeesCreateComponent,
      },
      {
        path: 'employees/:id',
        component: EmployeesDetailsComponent,
      },
      {
        path: 'employees/edit/:id',
        component: EmployeesEditComponent,
      },

      {
        path: 'menus',
        component: MenusComponent,
      },
      {
        path: 'menus/create',
        component: MenusCreateComponent,
      },
      {
        path: 'menus/:id',
        component: MenusDetailsComponent,
      },
      {
        path: 'menus/edit/:id',
        component: MenusEditComponent,
      },
      {
        path: 'menus/menu-items/:id',
        component: MenuItemDetailsComponent,
      },
      {
        path: 'menus/menu-items/edit/:id',
        component: MenuItemEditComponent,
      },

      {
        path: 'tables',
        component: TablesComponent,
      },
      {
        path: 'tables/create',
        component: TablesCreateComponent,
      },

      {
        path: 'tables/:restaurantId/qr-codes',
        component: TablesQrCodeComponent,
      },

      {
        path: 'orders',
        component: OrdersComponent,
      },
      {
        path: 'orders/history',
        component: OrdersHistoryComponent,
      },

      {
        path: 'chats',
        component: ChatsComponent,
      },

      {
        path: 'notifications',
        component: NotificationsComponent,
      },

      {
        path: 'settings',
        component: SettingsComponent,
      },
      {
        path: 'settings/blocked-users',
        component: BlockedUsersComponent,
      },
    ],
  },
  { path: 'login', component: LoginComponent, canActivate: [anonGuard] },
  ...employeeRoutes,
];
