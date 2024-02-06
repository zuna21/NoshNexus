import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideNavItemComponent } from '../side-nav-item/side-nav-item.component';
import { MatIconModule } from '@angular/material/icon';
import { SideNavDropdownComponent } from '../side-nav-dropdown/side-nav-dropdown.component';

@Component({
  selector: 'app-employee-side-nav',
  standalone: true,
  imports: [
    CommonModule,
    SideNavItemComponent,
    MatIconModule,
    SideNavDropdownComponent
  ],
  templateUrl: './employee-side-nav.component.html',
  styleUrls: ['./employee-side-nav.component.css']
})
export class EmployeeSideNavComponent {
  ordersLinkItems: { name: string; url: string }[] = [
    {
      name: 'live orders',
      url: '/employee/live-orders',
    },
    {
      name: 'history',
      url: '/employee/orders-history',
    },
  ];

  menusLinkItems: { name: string; url: string }[] = [
    {
      name: 'view menus',
      url: '/employee/menus',
    },
    {
      name: 'create menu',
      url: '/menus/create',
    },
  ];
}
