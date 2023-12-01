import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { SideNavItemComponent } from './side-nav-item/side-nav-item.component';
import { SideNavDropdownComponent } from './side-nav-dropdown/side-nav-dropdown.component';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    SideNavItemComponent,
    SideNavDropdownComponent,
  ],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css'],
})
export class SideNavComponent {
  restaurantsLinkItems: { name: string; url: string }[] = [
    {
      name: 'view restaurants',
      url: '/restaurants',
    }
  ];
  employeesLinkItems: { name: string; url: string }[] = [
    {
      name: 'view employees',
      url: '/employees',
    },
    {
      name: 'create employee',
      url: '/employees/create',
    },
  ];

  menusLinkItems: { name: string; url: string }[] = [
    {
      name: 'view menus',
      url: '/menus',
    },
    {
      name: 'create menu',
      url: '/menus/create',
    },
  ];

  tablesLinkItems: { name: string; url: string }[] = [
    {
      name: 'view tables',
      url: '/tables',
    },
    {
      name: 'create table',
      url: '/tables/create',
    },
  ];

  ordersLinkItems: { name: string; url: string }[] = [
    {
      name: 'live orders',
      url: '/orders',
    },
    {
      name: 'history',
      url: '/orders/history',
    },
  ];
}
