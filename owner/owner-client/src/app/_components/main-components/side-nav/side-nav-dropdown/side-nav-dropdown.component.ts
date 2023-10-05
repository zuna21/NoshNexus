import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink, RouterLinkActive } from '@angular/router';

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

  isOpen: boolean = false;
  totalDropdownHeight: string = '0px';


  ngOnInit(): void {
    this.calculateTotalDropdownHeight();
  }


  calculateTotalDropdownHeight() {
    this.totalDropdownHeight = `${this.itemListObj.length * DROPDOWN_ROW_HEIGHT}px`;
  }


}
