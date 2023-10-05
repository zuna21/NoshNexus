import { Component } from '@angular/core';
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
import {MatInputModule} from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { LightboxModule } from 'ng-gallery/lightbox';
import { DEFAULT_RESTAURANT_IMAGES } from 'src/app/_shared/default-restaurant-images';

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
    LightboxModule
  ],
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css'],
})
export class RestaurantsCreateComponent {
  restaurantImages: GalleryItem[] = [];
  countries: ICountry[] = [];
  profileImageURL: string = '';
  deleteDefaultImages: boolean = true;
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
    otherImages: [[]]
  });

  constructor(
    private fb: FormBuilder
  ) {
  }

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
    const fileHTML = event.target as HTMLInputElement;
    if (!fileHTML) return;
    if (!fileHTML.files) return;
    const file = fileHTML.files[0];
    this.restaurantFormGroup.patchValue({
      profilePicture: file,
    });

    this.profileImageURL = URL.createObjectURL(file);
  }


  uploadOtherPictures(event: any) {
    const fileHTML = event.target as HTMLInputElement;
    if (!fileHTML) return;
    if (!fileHTML.files) return;
    const files = fileHTML.files;
    if (files.length <= 0) return;
    if (this.deleteDefaultImages) this.restaurantImages = [];
    this.deleteDefaultImages = false;
    for (let i=0; i< files.length; i++) {
      const file = files[i];
      const images = this.restaurantFormGroup.get('otherImages')?.value;
      const newImages = [...images, file];
      this.restaurantFormGroup.patchValue({
        otherImages: newImages
      });
      
      const createdUrl = URL.createObjectURL(file);
      this.restaurantImages = [...this.restaurantImages, new ImageItem({src: createdUrl, thumb: createdUrl})];
    }

  }

  onSubmit() {
    if (this.restaurantFormGroup.invalid) return;
    console.log(this.restaurantFormGroup.value);
  }
}
