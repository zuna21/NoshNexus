import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { MenuItemCardComponent } from '../../components/menu-item-card/menu-item-card.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { OrderService } from '../../services/order.service';
import { DecimalPipe, Location, TitleCasePipe, UpperCasePipe } from '@angular/common';
import { TableService } from '../../services/table.service';
import { ITable } from '../../interfaces/table.interface';
import { Subscription, mergeMap, of } from 'rxjs';
import { IOrder } from '../../interfaces/order.interface';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AccountService } from '../../services/account.service';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../../components/login-dialog/login-dialog.component';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar'; 
import { TranslateModule } from '@ngx-translate/core';

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
    DragDropModule,
    MatSnackBarModule,
    TranslateModule,
    TitleCasePipe,
    UpperCasePipe
  ],
  templateUrl: './order-preview.component.html',
  styleUrl: './order-preview.component.css',
})
export class OrderPreviewComponent implements OnInit, OnDestroy {
  tables = signal<ITable[]>([]);
  order = signal<IOrder | null>(null);
  infoForm: FormGroup = this.fb.group({
    tableId: [null, Validators.required],
    note: [''],
  });
  restaurantId?: number;
  mousePosition = {
    x: 0,
    y: 0,
  };

  orderSub?: Subscription;
  tableSub?: Subscription;
  createOrderSub?: Subscription;
  dialogSub?: Subscription;

  constructor(
    public orderService: OrderService,
    public tableService: TableService,
    private fb: FormBuilder,
    private accountService: AccountService,
    private dialog: MatDialog,
    private location: Location,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.getOrder();
  }

  getOrder() {
    this.orderSub = this.orderService.order$
      .pipe(
        mergeMap((order) => {
          this.order.set(order);
          if (order.menuItems.length <= 0) return of(null);
          this.restaurantId = order.menuItems[0].restaurantId;
          return this.tableService.getTables(this.restaurantId); // Uvijek stolove za restoran od prvog menu itema
        })
      )
      .subscribe({
        next: (tables) => {
          if (!tables) return;
          this.tables.set(tables);
        },
      });
  }

  mouseDown($event: MouseEvent) {
    this.mousePosition.x = $event.screenX;
    this.mousePosition.y = $event.screenY;
  }

  onOrder($event: MouseEvent) {
    if (
      this.mousePosition.x === $event.screenX &&
      this.mousePosition.y === $event.screenY
    ) {
      if (!this.accountService.isLoggedIn()) {
        const dialogRef = this.dialog.open(LoginDialogComponent);
        this.dialogSub = dialogRef.afterClosed().subscribe({
          next: isLoggedIn => {
            if (!isLoggedIn) return;
            else this.createOrder();
          }
        })
      } else {
        this.createOrder();
      }
    }
  }

  onRemoveMenuItem(menuItemId: number) {
    this.orderService.removeMenuItem(menuItemId);
  }

  createOrder() {
    if (!this.restaurantId || this.infoForm.invalid) return;
    this.orderService.addNote(this.infoForm.get('note')?.value);
    this.orderService.selectTable(this.infoForm.get('tableId')?.value);
    this.createOrderSub = this.orderService.createOrder(this.restaurantId).subscribe({
      next: answer => {
        if (!answer) return;
        this.location.back();
        this.snackBar.open("Successfully created order.", "Ok");
        this.orderService.resetOrder();
      }
    })
  }

  ngOnDestroy(): void {
    this.orderSub?.unsubscribe();
  }
}
