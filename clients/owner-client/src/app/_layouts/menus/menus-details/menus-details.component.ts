import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { MatTabsModule } from '@angular/material/tabs';
import { MenuItemCreateComponent } from '../menu-item-create/menu-item-create.component';
import { MenuItemListComponent } from '../menu-item-list/menu-item-list.component';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Subscription, mergeMap, of } from 'rxjs';
import { IMenuDetails, IMenuItemCard } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-menus-details',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatTabsModule,
    MenuItemCreateComponent,
    MenuItemListComponent,
    MatDialogModule,
    RouterLink
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
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getMenu();
  }

  getMenu() {
    this.menuId = this.activatedRoute.snapshot.params['id'];
    if (!this.menuId) return;
    this.menuSub = this.menuService.getMenu(this.menuId).subscribe({
      next: menu => this.menu = menu
    });
  }

  dialogRefSub: Subscription | undefined;
  onDeleteMenu() {
    if (!this.menu) return;
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to delete ${this.menu.name}?`
    };

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    this.dialogRefSub = dialogRef.afterClosed().pipe(
      mergeMap(answer => {
        if (!answer || !this.menu) return of(null);
        return this.menuService.delete(this.menu.id);
      })
    ).subscribe({
      next: deletedMenuId => {
        if (!deletedMenuId) return;
        this.router.navigateByUrl(`/menus`);
      }
    });
  }

  menuItemCreated(menuItem: IMenuItemCard) {
    if (!this.menu) return;
    this.menu.menuItems = [...this.menu.menuItems, menuItem];
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
    this.dialogRefSub?.unsubscribe();
  }
}