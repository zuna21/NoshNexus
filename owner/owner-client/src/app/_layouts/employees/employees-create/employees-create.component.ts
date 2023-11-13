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
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { IImageCard } from 'src/app/_interfaces/IImage';
import { v4 as uuid } from 'uuid';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Subscription } from 'rxjs';

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
    ImageWithDeleteComponent,
    ReactiveFormsModule,
  ],
  templateUrl: './employees-create.component.html',
  styleUrls: ['./employees-create.component.css'],
})
export class EmployeesCreateComponent implements OnInit, OnDestroy {
  hidePassword: boolean = true;
  profilePhoto: IImageCard = {
    id: uuid(),
    url: 'assets/img/default-profile.png',
    size: 0,
    onClient: true
  };
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

  constructor(
    private fb: FormBuilder,
    private restaurantService: RestaurantService
  ) {}

  ngOnInit(): void {
    this.getOwnerRestaurantsForSelect();
  }

  uploadProfilePhoto(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.profilePhoto = {
      id: uuid(),
      url: URL.createObjectURL(image),
      size: image.size,
      onClient: true
    };
  }

  onDeleteProfilePhoto() {
    this.profilePhoto = {
      id: uuid(),
      url: 'assets/img/default-profile.png',
      size: 0,
      onClient: true
    };
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
    console.log(this.employeeForm.value);
  }

  ngOnDestroy(): void {
    this.restaurantSelectSub?.unsubscribe();
  }
}
