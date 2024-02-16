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
import { RestaurantStore } from 'src/app/_store/restaurant.store';
import { TranslateModule } from '@ngx-translate/core';

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
    ZipCodeDirective,
    TranslateModule
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
    latitude: [0.0, Validators.required],
    longitude: [0.0, Validators.required],
    websiteUrl: [''],
    isActive: [false, Validators.required],
  });
  
  getRestaurantCreateSub: Subscription | undefined;
  createSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder, 
    private snackBar: MatSnackBar,
    private restaurantService: RestaurantService,
    private router: Router,
    private restaurantStore: RestaurantStore
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



  onSubmit() {
    if (this.restaurantForm.invalid) {
      this.snackBar.open('Something went wrong.', 'Ok', {
        panelClass: 'warning-snackbar',
      });
      return;
    }

    this.createSub = this.restaurantService.create(this.restaurantForm.value).subscribe({
      next: restaurantForSelect => {
        if (!restaurantForSelect) return;
        this.restaurantStore.addRestaurantForSelect(restaurantForSelect);
        this.router.navigateByUrl(`restaurants/edit/${restaurantForSelect.id}`);
        this.snackBar.open("Successfully created restaurant", "Ok", { duration: 2000, panelClass: 'success-snackbar' });
      }
    });
  }


  ngOnDestroy(): void {
    this.getRestaurantCreateSub?.unsubscribe();
    this.createSub?.unsubscribe();
  }
}
