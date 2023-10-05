import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { SideNavItemComponent } from './side-nav-item/side-nav-item.component';
import { SideNavDropdownComponent } from './side-nav-dropdown/side-nav-dropdown.component';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [CommonModule, MatIconModule, SideNavItemComponent, SideNavDropdownComponent],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {
  restaurantsLinkItems: { name: string; url: string }[] = [
    {
      name: 'view restaurants',
      url: '/restaurants'
    },
    {
      name: 'create restaurant',
      url: '/restaurants/create'
    }
  ]


}
