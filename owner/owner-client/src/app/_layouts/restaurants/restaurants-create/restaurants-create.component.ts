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
    MatButtonModule
  ],
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css'],
})
export class RestaurantsCreateComponent {
  restaurantImages: GalleryItem[] = [];
  countries: ICountry[] = [];
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
  });

  constructor(
    private fb: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.loadRestaurantImages();
    this.loadCountries();
  }

  loadCountries() {
    this.countries = structuredClone(COUNTRIES);
  }

  loadRestaurantImages() {
    this.restaurantImages = [
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
      new ImageItem({
        src: 'assets/img/default.png',
        thumb: 'assets/img/default.png',
      }),
    ];
  }

  imageURL: string = '';
  showPreview(event: any) {
    const fileHTML = event.target as HTMLInputElement;
    if (!fileHTML) return;
    if (!fileHTML.files) return;
    const file = fileHTML.files[0];
    this.restaurantFormGroup.patchValue({
      profilePicture: file,
    });

    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    };
    reader.readAsDataURL(file);
    console.log(this.imageURL);
  }

  onSubmit() {
    if (this.restaurantFormGroup.invalid) return;
    console.log(this.restaurantFormGroup.value);
  }
}
