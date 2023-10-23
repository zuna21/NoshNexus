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
import { MatSelectModule } from '@angular/material/select';
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
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { Subscription } from 'rxjs';
import { ITableCard } from 'src/app/_interfaces/ITable';

@Component({
  selector: 'app-tables-create',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
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
    restaurant: [null, Validators.required],
  });
  restaurants: IRestaurantSelect[] = [];
  tables: ITableCard[] = [];

  restaurantSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private restaurantService: RestaurantService
  ) {}

  ngOnInit(): void {
    this.getRestaurants();
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService
      .getOwnerRestaurantsForSelect()
      .subscribe({
        next: (restaurants) => (this.restaurants = restaurants),
      });
  }


  onAddTable() {
    if (!this.tableForm.valid) return;
    const restaurant = this.restaurants.find(
      (x) => x.id === this.tableForm.get('restaurant')?.value
    );
    if (!restaurant) return;
    const table: ITableCard = {
      id: '',
      name: this.tableForm.get('name')?.value,
      restaurant: restaurant,
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
    // stolove poslati backendu
    console.log(this.tables);
  }


  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }
}
