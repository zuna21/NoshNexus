import { Component, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormGroup,
  Validators,
  FormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { ICountry } from 'src/app/_interfaces/ICountry';
import { COUNTRIES } from 'src/app/fake_data/countries';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { LightboxModule } from 'ng-gallery/lightbox';
import { DEFAULT_RESTAURANT_IMAGES } from 'src/app/_shared/default-restaurant-images';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ZipCodeDirective } from 'src/app/_directives/zip-code.directive';

@Component({
  selector: 'app-restaurants-create',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatIconModule,
    GalleryModule,
    MatInputModule,
    MatButtonModule,
    LightboxModule,
    MatSnackBarModule,
    ZipCodeDirective
  ],
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css'],
})
export class RestaurantsCreateComponent {
  restaurantImages: GalleryItem[] = [];
  countries: ICountry[] = [];
  profileImageURL: string = '';
  deleteDefaultImages: boolean = true;
  progressBarValue: number = 0;
  restaurantFormGroup: FormGroup = this.fb.group({
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
    profilePicture: [null],
    otherImages: [[]],
  });

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCountries();
    this.loadRestaurantImages();
  }


  loadCountries() {
    this.countries = structuredClone(COUNTRIES);
  }

  loadRestaurantImages() {
    this.restaurantImages = [...DEFAULT_RESTAURANT_IMAGES];
  }

  uploadProfilePicture(event: any) {
    const inputHtml = event.target as HTMLInputElement;
    if (!inputHtml || !inputHtml.files || inputHtml.files.length <= 0) {
      this.snackBar.open('Please upload image', 'Ok', {
        duration: 3000,
        panelClass: 'info-snackbar',
      });
      return;
    }
    const file = inputHtml.files[0];
    this.restaurantFormGroup.patchValue({
      profilePicture: file,
    });

    this.profileImageURL = URL.createObjectURL(file);
    this.snackBar.open('Successfully added restaurant profile image!', 'Ok', {
      duration: 2000,
      panelClass: 'success-snackbar',
    });
  }

  uploadOtherPictures(event: any) {
    const inputHtml = event.target as HTMLInputElement;
    if (!inputHtml || !inputHtml.files || inputHtml.files.length <= 0) {
      this.snackBar.open('Please upload images', 'Ok', {
        duration: 3000,
        panelClass: 'info-snackbar',
      });
      return;
    }
    const files = inputHtml.files;
    if (this.deleteDefaultImages) this.restaurantImages = [];
    this.deleteDefaultImages = false;
    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      const images = this.restaurantFormGroup.get('otherImages')?.value;
      const newImages = [...images, file];
      this.restaurantFormGroup.patchValue({
        otherImages: newImages,
      });

      const createdUrl = URL.createObjectURL(file);
      this.restaurantImages = [
        ...this.restaurantImages,
        new ImageItem({ src: createdUrl, thumb: createdUrl }),
      ];
    }

    this.snackBar.open('Successfully added restaurant images!', 'Ok', {
      duration: 2000,
      panelClass: 'success-snackbar',
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
      console.log('Desila se promjena');
  }

  onSubmit() {
    if (this.restaurantFormGroup.invalid) {
      this.snackBar.open('Something went wrong.', 'Ok', {
        panelClass: 'warning-snackbar',
      });
      return;
    }

    this.router.navigateByUrl('/restaurants');
    this.snackBar.open("Successfully created restaurant", "Ok", {duration: 2000, panelClass: 'success-snackbar'});
  }
}
