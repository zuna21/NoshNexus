import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { MenuItemCardComponent } from '../../components/menu-item-card/menu-item-card.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { OrderService } from '../../services/order.service';
import { DecimalPipe } from '@angular/common';
import { TableService } from '../../services/table.service';
import { ITable } from '../../interfaces/table.interface';
import { Subscription, mergeMap, of } from 'rxjs';
import { IOrder } from '../../interfaces/order.interface';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {DragDropModule} from '@angular/cdk/drag-drop'; 

@Component({
  selector: 'app-order-preview',
  standalone: true,
  imports: [
    MenuItemCardComponent,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    DecimalPipe,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    DragDropModule
  ],
  templateUrl: './order-preview.component.html',
  styleUrl: './order-preview.component.css'
})
export class OrderPreviewComponent implements OnInit, OnDestroy {
  tables = signal<ITable[]>([]);
  order = signal<IOrder | null>(null);
  infoForm: FormGroup = this.fb.group({
    tableId: [null, Validators.required],
    note: ['']
  });

  orderSub?: Subscription;
  tableSub?: Subscription;

  constructor(
    public orderService: OrderService,
    public tableService: TableService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getOrder();
  }

  getOrder() {
    this.orderSub = this.orderService.order$.pipe(
      mergeMap(order => {
        this.order.set(order);
        if (order.menuItems.length <= 0) return of(null);
        return this.tableService.getTables(order.menuItems[0].restaurantId); // Uvijek stolove za restoran od prvog menu itema
      })
    ).subscribe({
      next: tables => {
        if (!tables) return;
        this.tables.set(tables);
      }
    });
  }

  mousePosition = {
    x: 0,
    y: 0
  };
  
  mouseDown($event: MouseEvent) {
    this.mousePosition.x = $event.screenX;
    this.mousePosition.y = $event.screenY;
  }
  
  onOrder($event: MouseEvent) {
    if (
      this.mousePosition.x === $event.screenX &&
      this.mousePosition.y === $event.screenY
    ) {
      // Execute Click
      console.log('Make order')
    }
  }


  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }

}
