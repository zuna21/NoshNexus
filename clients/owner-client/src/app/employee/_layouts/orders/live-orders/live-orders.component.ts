import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderHubService } from 'src/app/_services/hubs/order-hub.service';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-live-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './live-orders.component.html',
  styleUrls: ['./live-orders.component.css']
})
export class LiveOrdersComponent implements OnInit, OnDestroy {

  constructor(
    private orderHub: OrderHubService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.startConnection();
  }

  async startConnection() {
    const token = this.accountService.getToken();
    if (!token) return;
    await this.orderHub.startConnection(token);
  }

  ngOnDestroy(): void {
    this.orderHub.stopConnection();
  }
}
