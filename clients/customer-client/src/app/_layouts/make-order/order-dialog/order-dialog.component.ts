import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { OrderStore } from 'src/app/_stores/order.store';
import { IOrder } from 'src/app/_interfaces/IOrder';
import { MenuItemRowComponent } from 'src/app/_components/menu-item-row/menu-item-row.component';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IMenuItemRow } from 'src/app/_interfaces/IMenuItem';

@Component({
  selector: 'app-order-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MenuItemRowComponent,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './order-dialog.component.html',
  styleUrls: ['./order-dialog.component.css']
})
export class OrderDialogComponent implements OnInit, OnDestroy {
  order?: IOrder;

  orderSub?: Subscription;

  constructor(
    private orderStore: OrderStore,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getOrder();
  }

  getOrder() {
    this.orderSub = this.orderStore.order$.subscribe({
      next: order => {
        if (order.menuItems.length <= 0) {
          this.router.navigateByUrl('/restaurants');
          return;
        }
        this.order = order;
      }
    });
  }

  onDelete(index: number) {
    if (!this.order || this.order.menuItems.length <= 0) {
      this.router.navigateByUrl('/restaurants');
      return;
    }
    let newArray: IMenuItemRow[] = [];
    for (let i = 0; i < this.order.menuItems.length; i++) {
      if (i === index) continue;
      newArray.push(this.order.menuItems[i]);
    }
    const newOrder: IOrder = {
      ...this.order,
      menuItems: [...newArray]
    };

    this.orderStore.setOrder(newOrder);
  }


  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
