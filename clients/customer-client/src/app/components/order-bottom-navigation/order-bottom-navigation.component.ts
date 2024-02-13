import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatRippleModule} from '@angular/material/core';
import { OrderService } from '../../services/order.service';
import {MatBadgeModule} from '@angular/material/badge'; 
import { AsyncPipe } from '@angular/common';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-order-bottom-navigation',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
    MatRippleModule,
    MatBadgeModule,
    AsyncPipe
  ],
  templateUrl: './order-bottom-navigation.component.html',
  styleUrl: './order-bottom-navigation.component.css'
})
export class OrderBottomNavigationComponent implements OnInit, OnDestroy {
  menuItemNumber = signal<number>(0);

  orderSub?: Subscription;

  constructor(
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.getMenuItemNumber();
  }

  getMenuItemNumber() {
    this.orderSub = this.orderService.order$.subscribe({
      next: order => {
        this.menuItemNumber.set(order.totalMenuItems)
      }
    });
  }

  onResetOrder() {
    this.orderService.resetOrder();
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
