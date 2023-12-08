import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { OrderStore } from 'src/app/_stores/order.store';
import { IOrder } from 'src/app/_interfaces/IOrder';
import { MenuItemRowComponent } from 'src/app/_components/menu-item-row/menu-item-row.component';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IMenuItemRow } from 'src/app/_interfaces/IMenuItem';
import { ITable } from 'src/app/_interfaces/ITable';
import { TableService } from 'src/app/_services/table.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-order-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MenuItemRowComponent,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule
  ],
  templateUrl: './order-dialog.component.html',
  styleUrls: ['./order-dialog.component.css']
})
export class OrderDialogComponent implements OnInit, OnDestroy {
  order?: IOrder;
  tables: ITable[] = [];
  orderForm: FormGroup = this.fb.group({
    tableId: [null, Validators.required],
    note: [''],
    menuItemIds: [[], Validators.required]
  });

  orderSub?: Subscription;
  tableSub?: Subscription;

  constructor(
    private orderStore: OrderStore,
    private router: Router,
    private tableService: TableService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.getOrder();
    this.getTables();
  }

  getOrder() {
    this.orderSub = this.orderStore.order$.subscribe({
      next: order => {
        if (order.menuItems.length <= 0) {
          this.router.navigateByUrl('/restaurants');
          return;
        }
        this.order = order;
        this.getMenuItemIds(this.order.menuItems);
      }
    });
  }

  getTables() {
    if (this.orderStore.getOrder().menuItems.length <= 0) {
      this.router.navigateByUrl('/restaurants');
      return;
    }
    const restaurantId = this.orderStore.getOrder().menuItems[0].restaurantId;
    if (!restaurantId) return;
    this.tableSub = this.tableService.getTables(restaurantId).subscribe({
      next: tables => this.tables = tables
    });
  }

  onDelete(index: number) {
    if (!this.order || this.order.menuItems.length <= 0) {
      this.router.navigateByUrl('/restaurants');
      return;
    }
    let newArray: IMenuItemRow[] = [];
    for (let i = 0; i < this.order.menuItems.length; i++) {
      if (i === index) continue;
      newArray.push(this.order.menuItems[i]);
    }
    const newOrder: IOrder = {
      ...this.order,
      menuItems: [...newArray]
    };
    this.orderStore.setOrder(newOrder);
    this.getMenuItemIds(newOrder.menuItems);
  }

  getMenuItemIds(menuItems: IMenuItemRow[]) {
    const menuItemIds: number[] = [];
    menuItems.map(x => menuItemIds.push(x.id));
    this.orderForm?.get('menuItemIds')?.patchValue(menuItemIds);
  }

  onSubmit() {
    if (!this.order || this.orderForm.invalid || this.order.menuItems.length <= 0) return;
    console.log(this.orderForm.value);
  }


  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
    this.tableSub?.unsubscribe();
  }
}
