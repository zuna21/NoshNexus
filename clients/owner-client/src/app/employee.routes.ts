import { Routes } from "@angular/router";
import { HomeComponent } from "./employee/_layouts/home/home.component";
import { MainComponentsComponent } from "./_components/main-components/main-components.component";
import { RestaurantComponent } from "./employee/_layouts/restaurant/restaurant.component";
import { LiveOrdersComponent } from "./employee/_layouts/orders/live-orders/live-orders.component";
import { OrdersHistoryComponent } from "./employee/_layouts/orders/orders-history/orders-history.component";
import { employeeGuard } from "./_guards/employee.guard";
import { AccountDetailsComponent } from "./employee/_layouts/account/account-details/account-details.component";
import { AccountEditComponent } from "./employee/_layouts/account/account-edit/account-edit.component";
import { MenusComponent } from "./employee/_layouts/menus/menus.component";
import { CreateMenuComponent } from "./employee/_layouts/menus/create-menu/create-menu.component";
import { MenuDetailsComponent } from "./employee/_layouts/menus/menu-details/menu-details.component";
import { EditMenuComponent } from "./employee/_layouts/menus/edit-menu/edit-menu.component";
import { MenuItemDetailsComponent } from "./_layouts/menus/menu-item-details/menu-item-details.component";
import { MenuItemEditComponent } from "./_layouts/menus/menu-item-edit/menu-item-edit.component";

export const employeeRoutes: Routes = [
    {
        path: 'employee', component: MainComponentsComponent,
        canActivate: [employeeGuard],
        children: [
            { path: '', pathMatch: 'full', redirectTo: '/employee/home' },
            { path: 'home', component: HomeComponent },

            { path: 'account-details', component: AccountDetailsComponent },
            { path: 'account-edit', component: AccountEditComponent },

            { path: 'restaurant', component: RestaurantComponent },

            { path: 'live-orders', component: LiveOrdersComponent },
            { path: 'orders-history', component: OrdersHistoryComponent },

            { path: 'menus', component: MenusComponent },
            { path: 'create-menu', component: CreateMenuComponent },
            { path: 'menus/:id', component: MenuDetailsComponent },
            { path: 'menus/edit/:menuId', component: EditMenuComponent },

            { path: 'menus/menu-items/:id', component: MenuItemDetailsComponent },
            { path: 'menus/menu-items/edit/:id', component: MenuItemEditComponent }
        ]
    }
]