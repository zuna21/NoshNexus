import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ARROW_DOWN_SVG, ARROW_UP_SVG } from 'src/app/_svgs/svg';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-side-nav-dropdown',
  standalone: true,
  imports: [CommonModule, MatIconModule, RouterLink],
  templateUrl: './side-nav-dropdown.component.html',
  styleUrls: ['./side-nav-dropdown.component.css']
})
export class SideNavDropdownComponent {
  @Input('itemListObj') itemListObj: {name: string; url: string}[] = [];
  @Input('name') name: string = '';
  arrowDownSvg: string = ARROW_DOWN_SVG;
  arrowUpSvg: string = ARROW_UP_SVG;
  isOpen: boolean = false;

  constructor(private iconRegistry: MatIconRegistry, private senitezer: DomSanitizer) {
    iconRegistry.addSvgIconLiteral('arrow-down-svg', senitezer.bypassSecurityTrustHtml(this.arrowDownSvg));
    iconRegistry.addSvgIconLiteral('arrow-up-svg', senitezer.bypassSecurityTrustHtml(this.arrowUpSvg));
  }
}
