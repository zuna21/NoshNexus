import { Routes } from '@angular/router';
import { HomeComponent } from './employee/_layouts/home/home.component';
import { MainComponentsComponent } from './_components/main-components/main-components.component';
import { RestaurantComponent } from './employee/_layouts/restaurant/restaurant.component';
import { LiveOrdersComponent } from './employee/_layouts/orders/live-orders/live-orders.component';
import { OrdersHistoryComponent } from './employee/_layouts/orders/orders-history/orders-history.component';
import { employeeGuard } from './_guards/employee.guard';
import { AccountDetailsComponent } from './employee/_layouts/account/account-details/account-details.component';
import { AccountEditComponent } from './employee/_layouts/account/account-edit/account-edit.component';
import { MenusComponent } from './employee/_layouts/menus/menus.component';
import { CreateMenuComponent } from './employee/_layouts/menus/create-menu/create-menu.component';
import { MenuDetailsComponent } from './employee/_layouts/menus/menu-details/menu-details.component';
import { EditMenuComponent } from './employee/_layouts/menus/edit-menu/edit-menu.component';
import { MenuItemDetailsComponent } from './_layouts/menus/menu-item-details/menu-item-details.component';
import { MenuItemEditComponent } from './_layouts/menus/menu-item-edit/menu-item-edit.component';

export const employeeRoutes: Routes = [
  {
    path: 'employee',
    component: MainComponentsComponent,
    canActivate: [employeeGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: '/employee/home' },
      {
        path: 'home',
        loadComponent: () =>
          import('./employee/_layouts/home/home.component').then(
            (com) => com.HomeComponent
          ),
      },

      {
        path: 'account-details',
        loadComponent: () =>
          import(
            './employee/_layouts/account/account-details/account-details.component'
          ).then((com) => com.AccountDetailsComponent),
      },
      {
        path: 'account-edit',
        loadComponent: () =>
          import(
            './employee/_layouts/account/account-edit/account-edit.component'
          ).then((com) => com.AccountEditComponent),
      },

      {
        path: 'restaurant',
        loadComponent: () =>
          import('./employee/_layouts/restaurant/restaurant.component').then(
            (com) => com.RestaurantComponent
          ),
      },

      {
        path: 'live-orders',
        loadComponent: () =>
          import(
            './employee/_layouts/orders/live-orders/live-orders.component'
          ).then((com) => com.LiveOrdersComponent),
      },

      {
        path: 'orders-history',
        loadComponent: () =>
          import(
            './employee/_layouts/orders/orders-history/orders-history.component'
          ).then((com) => com.OrdersHistoryComponent),
      },

      {
        path: 'menus',
        loadComponent: () =>
          import('./employee/_layouts/menus/menus.component').then(
            (com) => com.MenusComponent
          ),
      },

      {
        path: 'create-menu',
        loadComponent: () =>
          import(
            './employee/_layouts/menus/create-menu/create-menu.component'
          ).then((com) => com.CreateMenuComponent),
      },

      {
        path: 'menus/:id',
        loadComponent: () =>
          import(
            './employee/_layouts/menus/menu-details/menu-details.component'
          ).then((com) => com.MenuDetailsComponent),
      },

      {
        path: 'menus/edit/:menuId',
        loadComponent: () =>
          import(
            './employee/_layouts/menus/edit-menu/edit-menu.component'
          ).then((com) => com.EditMenuComponent),
      },

      {
        path: 'menus/menu-items/:id',
        loadComponent: () =>
          import(
            './_layouts/menus/menu-item-details/menu-item-details.component'
          ).then((com) => com.MenuItemDetailsComponent),
      },

      {
        path: 'menus/menu-items/edit/:id',
        loadComponent: () =>
          import(
            './_layouts/menus/menu-item-edit/menu-item-edit.component'
          ).then((com) => com.MenuItemEditComponent),
      },
    ],
  },
];
