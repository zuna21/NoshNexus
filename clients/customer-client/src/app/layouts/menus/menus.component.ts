import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { MenuCardComponent } from '../../components/menu-card/menu-card.component';
import { IMenuCard } from '../../interfaces/menu.interface';
import { Subscription } from 'rxjs';
import { MenuService } from '../../services/menu.service';
import { ActivatedRoute } from '@angular/router';

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
    private activatedRoute: ActivatedRoute
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
  
  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
  }
}
