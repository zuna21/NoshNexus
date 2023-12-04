import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TableCardComponent } from 'src/app/_components/table-card/table-card.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { IRestaurantDetails } from 'src/app/_interfaces/IRestaurant';
import { Subscription } from 'rxjs';
import { ITableCard } from 'src/app/_interfaces/ITable';
import { TableService } from 'src/app/_services/table.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tables-create',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    TableCardComponent,
    ReactiveFormsModule,
  ],
  templateUrl: './tables-create.component.html',
  styleUrls: ['./tables-create.component.css'],
})
export class TablesCreateComponent implements OnInit, OnDestroy {
  @ViewChild('inputName') inputName: ElementRef | undefined;
  tableForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    restaurant: [-1],
  });
  tables: ITableCard[] = [];
  restaurant: IRestaurantDetails | undefined;

  createTableSub: Subscription | undefined;
  restaurantSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private restaurantService: RestaurantService,
    private tableService: TableService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurantSub = this.restaurantService.getRestaurant().subscribe({
      next: restaurant => this.restaurant = restaurant
    });
  }

  onAddTable() {
    if (!this.tableForm.valid || !this.restaurant) return;
    const table: ITableCard = {
      id: -1,
      name: this.tableForm.get('name')?.value,
      restaurant: {
        id: this.restaurant.id,
        name: this.restaurant.name
      },
    };
    this.tables.push(table);
    this.tableForm.get('name')?.reset();
    if (!this.inputName) return;
    this.inputName.nativeElement.focus();
  }

  onRemoveTable(tableIndex: number) {
    this.tables.splice(tableIndex, 1);
  }

  onSubmit() {
    if (this.tables.length <= 0) return;
    this.createTableSub = this.tableService.create(this.tables).subscribe({
      next: isCreated => {
        if(!isCreated) return;
        this.router.navigateByUrl(`/tables`);
      }
    });
  }


  ngOnDestroy(): void {
    this.createTableSub?.unsubscribe();
    this.restaurantSub?.unsubscribe();
  }
}
