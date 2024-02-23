import { Component, OnDestroy, OnInit } from '@angular/core';

import {MatTabsModule} from '@angular/material/tabs';
import { MenuItemsComponent } from '../menu-items/menu-items.component';
import { MenusComponent } from '../menus/menus.component';
import { OrderBottomNavigationComponent } from '../../components/order-bottom-navigation/order-bottom-navigation.component';
import { TranslateModule } from '@ngx-translate/core';
import { TitleCasePipe } from '@angular/common';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { OrderService } from '../../services/order.service';


@Component({
  selector: 'app-selection',
  standalone: true,
  imports: [
    MatTabsModule,
    MenuItemsComponent,
    MenusComponent,
    OrderBottomNavigationComponent,
    TranslateModule,
    TitleCasePipe
  ],
  templateUrl: './selection.component.html',
  styleUrl: './selection.component.css'
})
export class SelectionComponent implements OnInit, OnDestroy {

  tableId?: number;
  tableIdSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.getTableId();
  }

  // Ovo je za QR code
  getTableId() {
    this.tableIdSub = this.activatedRoute.queryParams.subscribe({
      next: (qparams: Params) => {
        if (qparams['tableId']) {
          this.tableId = parseInt(qparams['tableId']);
          if (this.orderService.getTable() !== -1) return; // Ako je sto odabran neki drugi da ne vraca na onaj iz qr coda
          this.orderService.selectTable(this.tableId);
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.tableIdSub?.unsubscribe();
  }

}
