import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ARROW_DOWN_SVG, ARROW_UP_SVG } from 'src/app/_svgs/svg';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

const DROPDOWN_ROW_HEIGHT: number = 35;

@Component({
  selector: 'app-side-nav-dropdown',
  standalone: true,
  imports: [CommonModule, MatIconModule, RouterLink, RouterLinkActive],
  templateUrl: './side-nav-dropdown.component.html',
  styleUrls: ['./side-nav-dropdown.component.css'],
})
export class SideNavDropdownComponent implements OnInit {
  @Input('itemListObj') itemListObj: { name: string; url: string }[] = [];
  @Input('name') name: string = '';

  arrowDownSvg: string = ARROW_DOWN_SVG;
  arrowUpSvg: string = ARROW_UP_SVG;
  isOpen: boolean = false;
  totalDropdownHeight: string = '0px';

  constructor(
    private iconRegistry: MatIconRegistry,
    private senitezer: DomSanitizer,
    private router: Router
  ) {
    iconRegistry.addSvgIconLiteral(
      'arrow-down-svg',
      senitezer.bypassSecurityTrustHtml(this.arrowDownSvg)
    );
    iconRegistry.addSvgIconLiteral(
      'arrow-up-svg',
      senitezer.bypassSecurityTrustHtml(this.arrowUpSvg)
    );
  }

  ngOnInit(): void {
    this.calculateTotalDropdownHeight();
  }


  calculateTotalDropdownHeight() {
    this.totalDropdownHeight = `${this.itemListObj.length * DROPDOWN_ROW_HEIGHT}px`;
  }


}
