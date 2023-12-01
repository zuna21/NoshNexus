import { Component, Input, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemCardComponent } from 'src/app/_components/menu-item-card/menu-item-card.component';
import { IMenuItemCard } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';
import { Subscription } from 'rxjs';

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
    private menuService: MenuService
  ) {}

  onDelete(menuItemId: number) {
    this.menuItemDeleteSub = this.menuService.deleteMenuItem(menuItemId).subscribe({
      next: deletedMenuItemId => {
        this.menuItems = this.menuItems.filter(x => x.id !== deletedMenuItemId);
      }
    });
  }

  ngOnDestroy(): void {
    this.menuItemDeleteSub?.unsubscribe();
  }
}
