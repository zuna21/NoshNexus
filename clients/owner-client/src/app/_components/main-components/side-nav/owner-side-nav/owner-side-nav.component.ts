import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideNavItemComponent } from '../side-nav-item/side-nav-item.component';
import { MatIconModule } from '@angular/material/icon';
import { SideNavDropdownComponent } from '../side-nav-dropdown/side-nav-dropdown.component';

@Component({
  selector: 'app-owner-side-nav',
  standalone: true,
  imports: [
    CommonModule,
    SideNavItemComponent,
    MatIconModule,
    SideNavDropdownComponent
  ],
  templateUrl: './owner-side-nav.component.html',
  styleUrls: ['./owner-side-nav.component.css']
})
export class OwnerSideNavComponent {
  restaurantsLinkItems: { name: string; url: string }[] = [
    {
      name: 'view restaurants',
      url: '/restaurants',
    },
    {
      name: 'create restaurant',
      url: '/restaurants/create',
    },
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
