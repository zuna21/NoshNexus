import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderHubService } from 'src/app/_services/hubs/order-hub.service';
import { AccountService } from 'src/app/_services/account.service';
import { OrderService } from 'src/app/_services/order.service';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { Subscription, mergeMap, of } from 'rxjs';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { OrderDeclineDialogComponent } from 'src/app/_components/order-card/order-decline-dialog/order-decline-dialog.component';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';

@Component({
  selector: 'app-live-orders',
  standalone: true,
  imports: [
    CommonModule,
    OrderCardComponent
  ],
  templateUrl: './live-orders.component.html',
  styleUrls: ['./live-orders.component.css']
})
export class LiveOrdersComponent implements OnInit, OnDestroy {
  orders: IOrderCard[] = [];

  orderSub?: Subscription;
  acceptOrderSub?: Subscription;
  declineDialogSub?: Subscription;
  blockUserSub?: Subscription;
  newOrderSub?: Subscription;

  constructor(
    private orderHub: OrderHubService,
    private accountService: AccountService,
    private orderService: OrderService,
    private dialog: MatDialog
  ) {}

  async ngOnInit(): Promise<void> {
    await this.startConnection();
    
    this.getOrders();
    this.receiveNewOrder();
  }

  async startConnection() {
    const token = this.accountService.getToken();
    if (!token) return;
    await this.orderHub.startConnection(token);
  }

  getOrders() {
    this.orderSub = this.orderService.getEmployeeInProgressOrders().subscribe({
      next: orders => {
        this.orders = [...orders];
      }
    })
  }

  onAccept(orderCard: IOrderCard) {
    this.acceptOrderSub = this.orderService.accept(orderCard.id).subscribe({
      next: orderId => {
        if (!orderId) return;
        this.orders = this.orders.filter(x => x.id !== orderId);
      }
    });
  }

  onDecline(orderCard: IOrderCard) {
    const dialogRef = this.dialog.open(OrderDeclineDialogComponent);
    this.declineDialogSub = dialogRef.afterClosed().pipe(
      mergeMap((declineReason) => {
        if (!declineReason) return of(null);
        return this.orderService.decline(orderCard.id, declineReason);
      })
    ).subscribe({
      next: orderId => {
        this.orders = this.orders.filter(x => x.id !== orderId);
      }
    });
  }

  onBlockUser(orderCard: IOrderCard) {
    const config: MatDialogConfig = {
      data: `Are you sure you want to block ${orderCard.user.username}?`,
    };
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, config);
    this.blockUserSub = dialogRef
      .afterClosed()
      .pipe(
        mergeMap((response) => {
          if (!response) return of(null);
          return this.orderService.blockCustomer(orderCard.id);
        })
      )
      .subscribe({
        next: (orderId) => {
          if (!orderId) return;
          this.orders = this.orders.filter((x) => x.id !== orderId);
        },
      });
  }

  receiveNewOrder() {
    this.newOrderSub = this.orderHub.newOrder$.subscribe({
      next: order => {
        this.orders = [order, ...this.orders];
      }
    });
  }

  ngOnDestroy(): void {
    this.acceptOrderSub?.unsubscribe();
    this.declineDialogSub?.unsubscribe();
    this.blockUserSub?.unsubscribe();
    this.newOrderSub?.unsubscribe();

    this.orderHub.stopConnection();
  }
}
