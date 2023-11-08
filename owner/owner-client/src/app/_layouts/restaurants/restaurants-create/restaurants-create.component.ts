import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormGroup,
  Validators,
  FormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { ICountry } from 'src/app/_interfaces/ICountry';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { IImageCard } from 'src/app/_interfaces/IImage';
import { ZipCodeDirective } from 'src/app/_directives/zip-code.directive';
import { ICurrency } from 'src/app/_interfaces/ICurrency';
import { Subscription } from 'rxjs';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-restaurants-create',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
    MatSlideToggleModule,
    MatChipsModule,
    ZipCodeDirective
  ],
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css'],
})
export class RestaurantsCreateComponent implements OnInit, OnDestroy {
  countries: ICountry[] = [];
  currencies: ICurrency[] = [];
  progressBarValue: number = 0;

  otherImages: IImageCard[] = [];
  restaurantForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    countryId: [null, Validators.required],
    currencyId: [null, Validators.required],
    postalCode: [null, [Validators.required, Validators.minLength(5)]],
    phoneNumber: ['', Validators.required],
    city: ['', Validators.required],
    address: ['', Validators.required],
    description: [''],
    facebookUrl: [''],
    instagramUrl: [''],
    websiteUrl: [''],
    isActive: [false, Validators.required],
  });

  getRestaurantCreateSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder, 
    private snackBar: MatSnackBar,
    private restaurantService: RestaurantService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getRestaurantCreate();
  }


  getRestaurantCreate() {
    this.getRestaurantCreateSub = this.restaurantService.getRestaurantCreate().subscribe({
      next: restaurantCreate => {
        this.countries = restaurantCreate.countries;
        this.currencies = restaurantCreate.currencies;
      }
    });
  }



  createSub: Subscription | undefined;
  onSubmit() {
    if (this.restaurantForm.invalid) {
      this.snackBar.open('Something went wrong.', 'Ok', {
        panelClass: 'warning-snackbar',
      });
      return;
    }

    this.createSub = this.restaurantService.create(this.restaurantForm.value).subscribe({
      next: restaurantId => {
        if (!restaurantId) return;
        this.router.navigateByUrl(`restaurants/edit/${restaurantId}`);
        this.snackBar.open("Successfully created restaurant", "Ok", { duration: 2000, panelClass: 'success-snackbar' });
      }
    });
  }


  ngOnDestroy(): void {
    this.getRestaurantCreateSub?.unsubscribe();
    this.createSub?.unsubscribe();
  }
}
