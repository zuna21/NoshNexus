import {
  Component,
  OnDestroy,
  OnInit,
  signal,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItemService } from '../../services/menu-item.service';
import { IMenuItemCard } from '../../interfaces/menu-item.interface';
import { Subscription } from 'rxjs';
import { MenuItemCardComponent } from '../../components/menu-item-card/menu-item-card.component';
import { ScrollService } from '../main/scroll.service';

@Component({
  selector: 'app-menu-items',
  standalone: true,
  imports: [MenuItemCardComponent],
  templateUrl: './menu-items.component.html',
  styleUrl: './menu-items.component.css',
})
export class MenuItemsComponent implements OnInit, OnDestroy {
  restaurantId?: number;
  menuItems = signal<IMenuItemCard[]>([]);

  menuItemSub?: Subscription;
  scrollSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private menuItemService: MenuItemService,
    private scrollSerice: ScrollService
  ) {}

  ngOnInit(): void {
    this.getMenuItems();
    this.onScrollToBottom();
  }

  getMenuItems() {
    this.restaurantId = parseInt(
      this.activatedRoute.snapshot.params['restaurantId']
    );
    if (!this.restaurantId) return;
    console.log(this.restaurantId);
    this.menuItemSub = this.menuItemService
      .getRestaurantMenuItems(this.restaurantId)
      .subscribe({
        next: (menuItems) => this.menuItems.set(menuItems),
      });
  }

  onScrollToBottom() {
    this.scrollSub = this.scrollSerice.scolledToBottom$.subscribe({
      next: _ => console.log('Scrollao si do dna')
    });
  }


  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
    this.scrollSub?.unsubscribe();
  }
}
