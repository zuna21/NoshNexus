import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IMenuCard } from 'src/app/_interfaces/IMenu';
import { Subscription } from 'rxjs';
import { MenuService } from 'src/app/_services/menu.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuCardComponent } from 'src/app/_components/menu-card/menu-card.component';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';

@Component({
  selector: 'app-menus',
  standalone: true,
  imports: [
    CommonModule,
    MenuCardComponent,
    SearchBarComponent
  ],
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css']
})
export class MenusComponent implements OnInit, OnDestroy {
  menus: IMenuCard[] = [];
  menuSub?: Subscription;
  restaurantId?: number;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getMenus();
  }

  getMenus() {
    this.restaurantId = this.activatedRoute.snapshot.params['restaurantId'];
    if (!this.restaurantId) return;
    this.menuSub = this.menuService.getMenus(this.restaurantId).subscribe({
      next: menus => this.menus = menus
    });
  }

  onMenu(menuId: number) {
    if (!this.restaurantId) return;
    this.router.navigateByUrl(`/restaurants/${this.restaurantId}/make-order/menus/${menuId}`);
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
  }
}
