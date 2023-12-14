import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ILiveRestaurantOrders } from 'src/app/_interfaces/IOrder';
import { Subscription } from 'rxjs';
import { OrderService } from 'src/app/_services/order.service';
import { ActivatedRoute } from '@angular/router';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';
import { OrderHubService } from 'src/app/_services/order-hub.service';
import { AccountService } from 'src/app/_services/account.service';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar'; 

@Component({
  selector: 'app-live-orders',
  standalone: true,
  imports: [
    CommonModule,
    OrderCardComponent,
    MatSnackBarModule
  ],
  templateUrl: './live-orders.component.html',
  styleUrls: ['./live-orders.component.css'],
})
export class LiveOrdersComponent implements OnInit, OnDestroy {
  restaurantOrders?: ILiveRestaurantOrders;
  restaurantId?: number;

  restaurantOrderSub?: Subscription;
  newOrderSub?: Subscription;
  acceptOrderSub?: Subscription;
  removeOrderSub?: Subscription;
  
  constructor(
    private orderService: OrderService,
    private activatedRoute: ActivatedRoute,
    private orderHub: OrderHubService,
    private accountService: AccountService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.getRestaurantOrders();
    this.connectToOrderHub();
    this.receiveNewOrder();
    this.acceptOrder();
    this.removeOrder();
  }

  connectToOrderHub() {
    this.restaurantId = parseInt(
      this.activatedRoute.snapshot.params['restaurantId']
    );
    const token = this.accountService.getToken();
    if (!this.restaurantId || !token) return;
    this.orderHub.startConnection(token, this.restaurantId);
  }

  getRestaurantOrders() {
    this.restaurantId = this.activatedRoute.snapshot.params['restaurantId'];
    if (!this.restaurantId) return;
    this.restaurantOrderSub = this.orderService
      .getInProgressOrders(this.restaurantId)
      .subscribe({
        next: (restaurantOrders) => (this.restaurantOrders = restaurantOrders),
      });
  }

  receiveNewOrder() {
    this.newOrderSub = this.orderHub.newOrder$.subscribe({
      next: newOrder => {
        if (!this.restaurantOrders) return;
        this.restaurantOrders.orders = [...this.restaurantOrders.orders, newOrder];
      }
    });
  }

  acceptOrder() {
    this.acceptOrderSub = this.orderHub.acceptedOrderId$.subscribe({
      next: orderId => {
          if (!this.restaurantOrders) return;
          console.log('Ova je na accept');
          this.restaurantOrders.orders = this.restaurantOrders.orders.filter(x => {
            return x.id !== orderId
          });
          this.snackBar.open("Accepted order", "Ok", { duration: 2000, panelClass: 'success-snackbar' });
      }
    });
  }

  removeOrder() {
    this.removeOrderSub = this.orderHub.removedOrderId$.subscribe({
      next: removedOrderId => {
        console.log('Ova je na remove');
        if (!this.restaurantOrders) return;
        this.restaurantOrders.orders = this.restaurantOrders.orders.filter(x => {
          return x.id !== removedOrderId
        });
    }
    })
  }


  ngOnDestroy(): void {
    this.restaurantOrderSub?.unsubscribe();
    this.newOrderSub?.unsubscribe();
    this.acceptOrderSub?.unsubscribe();
    this.removeOrderSub?.unsubscribe();
    this.orderHub.stopConnection();
  }
}
