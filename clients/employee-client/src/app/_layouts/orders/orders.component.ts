import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { OrderService } from 'src/app/_services/order.service';
import { Subscription } from 'rxjs';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { OrderDeclineDialogComponent } from 'src/app/_components/order-card/order-decline-dialog/order-decline-dialog.component';
import { OrderHubService } from 'src/app/_services/order-hub.service';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, OrderCardComponent, MatDialogModule],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];

  orderSub?: Subscription;
  declineDialogSub?: Subscription;
  newOrderSub?: Subscription;
  acceptOrderSub?: Subscription;

  constructor(
    private orderService: OrderService, 
    private dialog: MatDialog,
    private orderHub: OrderHubService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getOrders();
    this.connectToOrderHub();
    this.receiveNewOrder();
  }

  connectToOrderHub() {
    const token = this.accountService.getToken();
    if (!token) return;
    this.orderHub.startConnection(token);
  }

  getOrders() {
    this.orderSub = this.orderService.getOwnerInProgressOrders().subscribe({
      next: (orders) => (this.orders = orders),
    });
  }

  onAccept(order: IOrderCard) {
    this.acceptOrderSub = this.orderService.acceptOrder(order.id).subscribe({
      next: acceptedOrderId => {
        this.orders = this.orders.filter(x => {
          return x.id !== acceptedOrderId
        });
      }
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

  receiveNewOrder() {
    this.newOrderSub = this.orderHub.newOrder$.subscribe({
      next: newOrder => this.orders = [...this.orders, newOrder]
    });
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.declineDialogSub?.unsubscribe();
    this.newOrderSub?.unsubscribe();

    this.orderHub.stopConnection();
  }
}
