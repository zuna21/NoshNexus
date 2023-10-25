import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import {MatSlideToggleModule} from '@angular/material/slide-toggle'; 
import { Subscription } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-menus-create',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatSlideToggleModule
  ],
  templateUrl: './menus-create.component.html',
  styleUrls: ['./menus-create.component.css'],
})
export class MenusCreateComponent implements OnInit, OnDestroy {
  restaurants: IRestaurantSelect[] = [];
  menuForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    restaurant: [null, Validators.required],
    isActive: [false, Validators.required]
  });

  restaurantSub: Subscription | undefined;

  constructor(
    private restaurantService: RestaurantService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getRestaurants();
  }

  getRestaurants() {
    this.restaurantSub = this.restaurantService
      .getOwnerRestaurantsForSelect()
      .subscribe({
        next: (restaurants) => {
          this.restaurants = restaurants;
        },
      });
  }

  ngOnDestroy(): void {
    this.restaurantSub?.unsubscribe();
  }

  onSubmit() {
    if (!this.menuForm.valid) return;
    console.log(this.menuForm.value);
  }
}
