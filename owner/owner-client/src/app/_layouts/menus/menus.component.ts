import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuCardComponent } from 'src/app/_components/menu-card/menu-card.component';
import { IMenuCard } from 'src/app/_interfaces/IMenu';
import { Subscription } from 'rxjs';
import { MenuService } from 'src/app/_services/menu.service';

@Component({
  selector: 'app-menus',
  standalone: true,
  imports: [CommonModule, MenuCardComponent],
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css']
})
export class MenusComponent implements OnInit, OnDestroy {
  menus: IMenuCard[] = [];

  menuSub: Subscription | undefined;

  constructor(
    private menuService: MenuService
  ) { }

  ngOnInit(): void {
    this.getMenus();
  }

  getMenus() {
    this.menuSub = this.menuService.getOwnerMenus().subscribe({
      next: menus => this.menus = menus
    });
  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
  }
}
