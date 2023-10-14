import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { MatTabsModule } from '@angular/material/tabs';
import { MenuItemCreateComponent } from '../menu-item-create/menu-item-create.component';
import { MenuItemListComponent } from '../menu-item-list/menu-item-list.component';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { IMenuDetails } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';

@Component({
  selector: 'app-menus-details',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatTabsModule,
    MenuItemCreateComponent,
    MenuItemListComponent,
  ],
  templateUrl: './menus-details.component.html',
  styleUrls: ['./menus-details.component.css'],
})
export class MenusDetailsComponent implements OnInit, OnDestroy {
  menu: IMenuDetails | undefined;
  menuId: string = '';

  menuSub: Subscription | undefined;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getMenu();
  }

  getMenu() {
    this.menuId = this.activatedRoute.snapshot.params['id'];
    if (!this.menuId) return;
    this.menuSub = this.menuService.getOwnerMenuDetails(this.menuId).subscribe({
      next: menu => this.menu = menu
    });
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
  }
}
