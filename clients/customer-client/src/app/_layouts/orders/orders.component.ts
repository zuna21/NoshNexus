import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatRadioModule} from '@angular/material/radio'; 
import { FormsModule } from '@angular/forms';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { OrderService } from 'src/app/_services/order.service';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { Subscription } from 'rxjs';
import { SharedCardsModule } from 'shared-cards';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    CommonModule,
    MatRadioModule,
    FormsModule,
    SearchBarComponent,
    SharedCardsModule
  ],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit, OnDestroy {
  selectedStatus: string = 'all';
  sq: string = '';
  orders: IOrderCard[] = [];

  orderSub?: Subscription;
  acceptedOrderSub?: Subscription;
  declinedOrderSub?: Subscription;

  constructor(
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderSub = this.orderService.getOrders(this.sq).subscribe({
      next: orders => this.orders = [...orders]
    });
  }

  getAcceptedOrder() {
    this.acceptedOrderSub = this.orderService.getAcceptedOrders(this.sq).subscribe({
      next: orders => this.orders = [...orders]
    });
  }

  getDeclinedOrders() {
    this.declinedOrderSub = this.orderService.getDeclinedOrders(this.sq).subscribe({
      next: orders => this.orders = [...orders]
    });
  }

  onSearch(sq: string) {
    this.sq = sq;
    this.onChangeStatus();
  }

  onChangeStatus() {
    switch (this.selectedStatus) {
      case 'accepted':
        this.getAcceptedOrder();
        break;
      case 'declined':
        this.getDeclinedOrders();
        break;
      default:
        this.getOrders();
        break;
    }
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.acceptedOrderSub?.unsubscribe();
    this.declinedOrderSub?.unsubscribe();
  }

}
