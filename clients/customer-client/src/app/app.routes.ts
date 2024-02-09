import { Routes } from '@angular/router';
import { HomeComponent } from './layouts/home/home.component';
import { MainComponent } from './layouts/main/main.component';

export const routes: Routes = [
    { path: '', component: MainComponent, children: [
        { path: '', pathMatch: 'full', redirectTo: '/home'},
        { path: 'home', component: HomeComponent }
    ] }
];
