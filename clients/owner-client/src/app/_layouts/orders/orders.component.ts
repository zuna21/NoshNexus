import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription } from 'rxjs';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { OrderDeclineDialogComponent } from 'src/app/_components/order-card/order-decline-dialog/order-decline-dialog.component';
import { SharedCardsModule } from 'shared-cards';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    CommonModule, 
    MatDialogModule,
    SharedCardsModule
  ],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];

  orderSub: Subscription | undefined;
  declineDialogSub: Subscription | undefined;

  constructor(private orderService: OrderService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderSub = this.orderService.getOwnerInProgressOrders().subscribe({
      next: (orders) => (this.orders = orders),
    });
  }

  onDecline(order: IOrderCard) {
    const dialogRef = this.dialog.open(OrderDeclineDialogComponent);
    this.declineDialogSub = dialogRef.afterClosed().subscribe({
      next: (declineReason) => {
        if (!declineReason) return;
        console.log(declineReason);
      },
    });
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.declineDialogSub?.unsubscribe();
  }
}
