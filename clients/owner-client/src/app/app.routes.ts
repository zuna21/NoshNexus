import { Routes } from '@angular/router';
import { LoginComponent } from './_layouts/login/login.component';
import { authGuard } from './_guards/auth.guard';
import { anonGuard } from './_guards/anon.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./_components/main-components/main-components.component').then(
        (mod) => mod.MainComponentsComponent
      ),
    canActivate: [authGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: '/home' },
      {
        path: 'home',
        loadComponent: () =>
          import('./_layouts/home/home.component').then(
            (mod) => mod.HomeComponent
          ),
      },

      {
        path: 'account',
        loadComponent: () =>
          import('./_layouts/account/account.component').then(
            (mod) => mod.AccountComponent
          ),
      },
      {
        path: 'account/edit',
        loadComponent: () =>
          import('./_layouts/account/account-edit/account-edit.component').then(
            (mod) => mod.AccountEditComponent
          ),
      },

      {
        path: 'charts/orders-by-day/:restaurantId',
        loadComponent: () =>
          import(
            './_layouts/charts/orders-by-day/orders-by-day.component'
          ).then((mod) => mod.OrdersByDayComponent),
      },

      {
        path: 'restaurants',
        loadComponent: () =>
          import('./_layouts/restaurants/restaurants.component').then(
            (mod) => mod.RestaurantsComponent
          ),
      },
      {
        path: 'restaurants/create',
        loadComponent: () =>
          import(
            './_layouts/restaurants/restaurants-create/restaurants-create.component'
          ).then((mod) => mod.RestaurantsCreateComponent),
      },
      {
        path: 'restaurants/:id',
        loadComponent: () =>
          import(
            './_layouts/restaurants/restaurants-details/restaurants-details.component'
          ).then((mod) => mod.RestaurantsDetailsComponent),
      },
      {
        path: 'restaurants/edit/:id',
        loadComponent: () =>
          import(
            './_layouts/restaurants/restaurants-edit/restaurants-edit.component'
          ).then((mod) => mod.RestaurantsEditComponent),
      },

      {
        path: 'employees',
        loadComponent: () =>
          import('./_layouts/employees/employees.component').then(
            (mod) => mod.EmployeesComponent
          ),
      },
      {
        path: 'employees/create',
        loadComponent: () =>
          import(
            './_layouts/employees/employees-create/employees-create.component'
          ).then((mod) => mod.EmployeesCreateComponent),
      },
      {
        path: 'employees/:id',
        loadComponent: () =>
          import(
            './_layouts/employees/employees-details/employees-details.component'
          ).then((mod) => mod.EmployeesDetailsComponent),
      },
      {
        path: 'employees/edit/:id',
        loadComponent: () =>
          import(
            './_layouts/employees/employees-edit/employees-edit.component'
          ).then((mod) => mod.EmployeesEditComponent),
      },

      {
        path: 'menus',
        loadComponent: () =>
          import('./_layouts/menus/menus.component').then(
            (mod) => mod.MenusComponent
          ),
      },
      {
        path: 'menus/create',
        loadComponent: () =>
          import('./_layouts/menus/menus-create/menus-create.component').then(
            (mod) => mod.MenusCreateComponent
          ),
      },
      {
        path: 'menus/:id',
        loadComponent: () =>
          import('./_layouts/menus/menus-details/menus-details.component').then(
            (mod) => mod.MenusDetailsComponent
          ),
      },
      {
        path: 'menus/edit/:id',
        loadComponent: () =>
          import('./_layouts/menus/menus-edit/menus-edit.component').then(
            (mod) => mod.MenusEditComponent
          ),
      },
      {
        path: 'menus/menu-items/:id',
        loadComponent: () =>
          import(
            './_layouts/menus/menu-item-details/menu-item-details.component'
          ).then((mod) => mod.MenuItemDetailsComponent),
      },
      {
        path: 'menus/menu-items/edit/:id',
        loadComponent: () =>
          import(
            './_layouts/menus/menu-item-edit/menu-item-edit.component'
          ).then((mod) => mod.MenuItemEditComponent),
      },

      {
        path: 'tables',
        loadComponent: () =>
          import('./_layouts/tables/tables.component').then(
            (mod) => mod.TablesComponent
          ),
      },
      {
        path: 'tables/create',
        loadComponent: () =>
          import(
            './_layouts/tables/tables-create/tables-create.component'
          ).then((mod) => mod.TablesCreateComponent),
      },

      {
        path: 'orders',
        loadComponent: () =>
          import('./_layouts/orders/orders.component').then(
            (mod) => mod.OrdersComponent
          ),
      },
      {
        path: 'orders/history',
        loadComponent: () =>
          import(
            './_layouts/orders/orders-history/orders-history.component'
          ).then((mod) => mod.OrdersHistoryComponent),
      },

      {
        path: 'chats',
        loadComponent: () =>
          import('./_layouts/chats/chats.component').then(
            (mod) => mod.ChatsComponent
          ),
      },

      {
        path: 'notifications',
        loadComponent: () =>
          import('./_layouts/notifications/notifications.component').then(
            (mod) => mod.NotificationsComponent
          ),
      },

      {
        path: 'settings',
        loadComponent: () =>
          import('./_layouts/settings/settings.component').then(
            (mod) => mod.SettingsComponent
          ),
      },
      {
        path: 'settings/blocked-users',
        loadComponent: () =>
          import(
            './_layouts/settings/blocked-users/blocked-users.component'
          ).then((mod) => mod.BlockedUsersComponent),
      },
    ],
  },
  { path: 'login', component: LoginComponent, canActivate: [anonGuard] },
];
