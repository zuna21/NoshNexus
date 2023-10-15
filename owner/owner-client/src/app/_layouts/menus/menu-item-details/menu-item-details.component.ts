import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { IMenuItemDetails } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-menu-item-details',
  standalone: true,
  imports: [
    CommonModule,
    MatDividerModule,
    MatIconModule,
    MatButtonModule,
    RouterLink
  ],
  templateUrl: './menu-item-details.component.html',
  styleUrls: ['./menu-item-details.component.css']
})
export class MenuItemDetailsComponent implements OnInit, OnDestroy {
  menuItem: IMenuItemDetails | undefined;
  menuItemId: string = '';

  menuItemSub: Subscription | undefined;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute
  ) { }



  ngOnInit(): void {
    this.getMenuItem();
  }

  getMenuItem() {
    this.menuItemId = this.activatedRoute.snapshot.params['id'];
    if (!this.menuItemId) return;
    this.menuItemSub = this.menuService.getOwnerMenuItemDetails(this.menuItemId).subscribe({
      next: menuItem => this.menuItem = menuItem
    });
  }

  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
  }

}
