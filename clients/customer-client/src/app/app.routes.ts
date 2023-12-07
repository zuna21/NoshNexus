import { Routes } from '@angular/router';
import { MainLayoutComponent } from './_layouts/main-layout/main-layout.component';
import { HomeComponent } from './_layouts/home/home.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', pathMatch: 'full', redirectTo: '/home' },
            { path: 'home', component: HomeComponent }
        ]
    }
];
