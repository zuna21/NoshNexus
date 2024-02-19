import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatRippleModule} from '@angular/material/core';
import { OrderService } from '../../services/order.service';
import {MatBadgeModule} from '@angular/material/badge'; 
import { AsyncPipe, TitleCasePipe } from '@angular/common';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';


@Component({
  selector: 'app-order-bottom-navigation',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
    MatRippleModule,
    MatBadgeModule,
    AsyncPipe,
    TranslateModule,
    TitleCasePipe
  ],
  templateUrl: './order-bottom-navigation.component.html',
  styleUrl: './order-bottom-navigation.component.css'
})
export class OrderBottomNavigationComponent implements OnInit, OnDestroy {
  menuItemNumber = signal<number>(0);

  orderSub?: Subscription;

  constructor(
    private orderService: OrderService,
    private router: Router
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

  onOpenOrderPreview() {
    this.router.navigateByUrl('/order-preview');
  }

  onResetOrder() {
    this.orderService.resetOrder();
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
