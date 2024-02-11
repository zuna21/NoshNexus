import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { MenuCardComponent } from '../../components/menu-card/menu-card.component';
import { IMenuCard } from '../../interfaces/menu.interface';
import { Subscription } from 'rxjs';
import { MenuService } from '../../services/menu.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-menus',
  standalone: true,
  imports: [
    MenuCardComponent
  ],
  templateUrl: './menus.component.html',
  styleUrl: './menus.component.css'
})
export class MenusComponent implements OnInit, OnDestroy {
  menus = signal<IMenuCard[]>([]);
  restaurantId?: number;

  menuSub?: Subscription;

  constructor(
    private menuService: MenuService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getMenus();
  }

  getMenus() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;
    this.menuSub = this.menuService.getRestaurantMenus(this.restaurantId).subscribe({
      next: menus => this.menus.set(menus)
    });
  }

  onViewMore(menuId: number) {
    if (!this.restaurantId) return;
    this.router.navigateByUrl(`/selection/${this.restaurantId}/${menuId}`);
  }
  
  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
  }
}
