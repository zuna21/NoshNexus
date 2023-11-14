import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Subscription } from 'rxjs';
import { EmployeeService } from 'src/app/_services/employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employees-create',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatCheckboxModule,
    ReactiveFormsModule,
  ],
  templateUrl: './employees-create.component.html',
  styleUrls: ['./employees-create.component.css'],
})
export class EmployeesCreateComponent implements OnInit, OnDestroy {
  hidePassword: boolean = true;
  employeeForm: FormGroup = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    phone: ['', Validators.required],
    city: ['', Validators.required],
    address: ['', Validators.required],
    restaurantId: ['', Validators.required],
    birth: [null, Validators.required],
    description: [''],
    canEditMenus: [false, Validators.required],
    canViewFolders: [false, Validators.required],
    canEditFolders: [false, Validators.required],
  });
  restaurantSelect: IRestaurantSelect[] = [];

  restaurantSelectSub: Subscription | undefined;
  createEmpoloyeeSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private restaurantService: RestaurantService,
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getOwnerRestaurantsForSelect();
  }



  getOwnerRestaurantsForSelect() {
    this.restaurantSelectSub = this.restaurantService
      .getOwnerRestaurantsForSelect()
      .subscribe({
        next: (restaurants) => (this.restaurantSelect = restaurants),
      });
  }

  onSubmit() {
    if (this.employeeForm.invalid) return;
    this.createEmpoloyeeSub = this.employeeService.create(this.employeeForm.value).subscribe({
      next: employeeId => {
        if (!employeeId) return;
        this.router.navigateByUrl(`/employees/edit/${employeeId}`);
      }
    });
  }

  ngOnDestroy(): void {
    this.restaurantSelectSub?.unsubscribe();
    this.createEmpoloyeeSub?.unsubscribe();
  }
}
