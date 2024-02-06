import { Component, Input, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemCardComponent } from 'src/app/_components/menu-item-card/menu-item-card.component';
import { IMenuItemCard } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-menu-item-list',
  standalone: true,
  imports: [CommonModule, MenuItemCardComponent],
  templateUrl: './menu-item-list.component.html',
  styleUrls: ['./menu-item-list.component.css'],
})
export class MenuItemListComponent implements OnDestroy {
  @Input('menuItems') menuItems: IMenuItemCard[] = [];

  menuItemDeleteSub: Subscription | undefined;

  constructor(
    private menuService: MenuService,
    private router: Router,
    private accountService: AccountService
  ) {}

  onDelete(menuItemId: number) {
    this.menuItemDeleteSub = this.menuService.deleteMenuItem(menuItemId).subscribe({
      next: deletedMenuItemId => {
        this.menuItems = this.menuItems.filter(x => x.id !== deletedMenuItemId);
      }
    });
  }

  onViewMore(menuId: number) {
    console.log(menuId);
    const role = this.accountService.getRole();
    if (role === 'owner') {
      this.router.navigateByUrl(`/menus/menu-items/${menuId}`);
    } else {
      this.router.navigateByUrl(`/employee/menus/menu-items/${menuId}`);
    }
  }

  ngOnDestroy(): void {
    this.menuItemDeleteSub?.unsubscribe();
  }
}
