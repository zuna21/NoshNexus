import { Routes } from "@angular/router";
import { HomeComponent } from "./employee/_layouts/home/home.component";
import { MainComponentsComponent } from "./_components/main-components/main-components.component";
import { RestaurantComponent } from "./employee/_layouts/restaurant/restaurant.component";
import { LiveOrdersComponent } from "./employee/_layouts/orders/live-orders/live-orders.component";
import { OrdersHistoryComponent } from "./employee/_layouts/orders/orders-history/orders-history.component";

export const employeeRoutes: Routes = [
    {
        path: 'employee', component: MainComponentsComponent, children: [
            { path: '', pathMatch: 'full', redirectTo: '/employee/home' },
            { path: 'home', component: HomeComponent },

            { path: 'restaurant', component: RestaurantComponent },

            { path: 'live-orders', component: LiveOrdersComponent },
            { path: 'orders-history', component: OrdersHistoryComponent }
        ]
    }
]