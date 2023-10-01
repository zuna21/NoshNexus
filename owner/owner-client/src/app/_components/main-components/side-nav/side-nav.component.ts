import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { SideNavItemComponent } from './side-nav-item/side-nav-item.component';
import { EMPLOYEES_SVG, HOUSE_SVG, MENUS_SVG, RESTAURANT_SVG } from 'src/app/_svgs/svg';
import { DomSanitizer } from '@angular/platform-browser';
import { SideNavDropdownComponent } from './side-nav-dropdown/side-nav-dropdown.component';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [CommonModule, MatIconModule, SideNavItemComponent, SideNavDropdownComponent],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {
  houseSvg: string = HOUSE_SVG;
  restaurantSvg: string = RESTAURANT_SVG;
  employeeSvg: string = EMPLOYEES_SVG;
  menuSvg: string = MENUS_SVG
  restaurantsLinkItems: {name: string; url: string}[] = [
    {
      name: 'view restaurants',
      url: '/restaurants'
    }
  ]

  constructor(private iconRegistry: MatIconRegistry, private senitezer: DomSanitizer) {
    iconRegistry.addSvgIconLiteral('house-svg', senitezer.bypassSecurityTrustHtml(this.houseSvg));
    iconRegistry.addSvgIconLiteral('restaurant-svg', senitezer.bypassSecurityTrustHtml(this.restaurantSvg));
    iconRegistry.addSvgIconLiteral('employees-svg', senitezer.bypassSecurityTrustHtml(this.employeeSvg));
    iconRegistry.addSvgIconLiteral('menus-svg', senitezer.bypassSecurityTrustHtml(this.menuSvg));
  }
}
