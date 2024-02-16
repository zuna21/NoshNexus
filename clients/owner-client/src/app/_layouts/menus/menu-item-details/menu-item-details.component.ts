import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { IGetMenuItem } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-menu-item-details',
  standalone: true,
  imports: [
    CommonModule,
    MatDividerModule,
    MatIconModule,
    MatButtonModule,
    TranslateModule
  ],
  templateUrl: './menu-item-details.component.html',
  styleUrls: ['./menu-item-details.component.css']
})
export class MenuItemDetailsComponent implements OnInit, OnDestroy {
  menuItem: IGetMenuItem | undefined;
  menuItemId: string = '';

  menuItemSub: Subscription | undefined;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute,
    private accountService: AccountService,
    private router: Router
  ) { }



  ngOnInit(): void {
    this.getMenuItem();
  }

  getMenuItem() {
    this.menuItemId = this.activatedRoute.snapshot.params['id'];
    if (!this.menuItemId) return;
    const isOwner = this.accountService.getRole() === 'owner';
    this.menuItemSub = this.menuService.getMenuItem(this.menuItemId, isOwner).subscribe({
      next: menuItem => this.menuItem = menuItem
    });
  }

  onEdit() {
    if (!this.menuItemId) return;
    const isOwner = this.accountService.getRole() === 'owner';
    if (isOwner) {
      this.router.navigateByUrl(`/menus/menu-items/edit/${this.menuItemId}`);
    } else {
      this.router.navigateByUrl(`/employee/menus/menu-items/edit/${this.menuItemId}`);
    }
  }

  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
  }

}
