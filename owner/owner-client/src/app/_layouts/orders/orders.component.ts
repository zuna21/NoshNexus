import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription } from 'rxjs';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { MenuItemDialogComponent } from 'src/app/_components/order-card/menu-item-dialog/menu-item-dialog.component';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports:
    [
      CommonModule,
      OrderCardComponent,
      MatDialogModule
    ],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];

  orderSub: Subscription | undefined;

  constructor(
    private orderService: OrderService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderSub = this.orderService.getOwnerOrders().subscribe({
      next: (orders) => (this.orders = orders),
    });
  }

  onViewMenuItems(order: IOrderCard) {
    const dialogConfig: MatDialogConfig = {
      data: order.totalItems
    };

    this.dialog.open(MenuItemDialogComponent, dialogConfig);
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
