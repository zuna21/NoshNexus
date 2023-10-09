import { Component } from '@angular/core';
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
import { Router } from '@angular/router';
import { ZipCodeDirective } from 'src/app/_directives/zip-code.directive';
import { COUNTRIES } from 'src/app/_shared/countries';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';

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
    MatChipsModule
  ],
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css'],
})
export class RestaurantsCreateComponent {
  countries: ICountry[] = [];
  progressBarValue: number = 0;
  restaurantForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    country: ['', Validators.required],
    postalCode: [null, [Validators.required, Validators.minLength(5)]],
    phone: ['', Validators.required],
    city: ['', Validators.required],
    address: ['', Validators.required],
    description: [''],
    facebookUrl: [''],
    instagramUrl: [''],
    websiteUrl: [''],
    isActive: [false, Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadCountries();
  }


  loadCountries() {
    this.countries = structuredClone(COUNTRIES);
  }


  onSubmit() {
    if (this.restaurantForm.invalid) {
      this.snackBar.open('Something went wrong.', 'Ok', {
        panelClass: 'warning-snackbar',
      });
      return;
    }

    this.router.navigateByUrl('/restaurants');
    this.snackBar.open("Successfully created restaurant", "Ok", { duration: 2000, panelClass: 'success-snackbar' });
  }
}
