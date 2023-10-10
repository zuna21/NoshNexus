import { Component, EventEmitter } from '@angular/core';
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
import { COUNTRIES } from 'src/app/_shared/countries';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { IImageCard } from 'src/app/_interfaces/IImage';
import { v4 as uuid } from 'uuid';
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
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
    MatSlideToggleModule,
    MatChipsModule,
    ImageWithDeleteComponent,
    ZipCodeDirective
  ],
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css'],
})
export class RestaurantsCreateComponent {
  countries: ICountry[] = [];
  progressBarValue: number = 0;
  profileImage: IImageCard = {
    id: uuid(),
    url: 'assets/img/default.png',
    size: 0,
  };
  otherImages: IImageCard[] = [];
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
    isActive: [false, Validators.required],
  });

  constructor(private fb: FormBuilder, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.loadCountries();
  }

  loadCountries() {
    this.countries = structuredClone(COUNTRIES);
  }

  uploadProfileImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.profileImage = {
      id: uuid(),
      url: URL.createObjectURL(image),
      size: image.size,
    };
  }

  uploadImages(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const images = inputHTML.files;
    for (let i = 0; i < images.length; i++) {
      const image: IImageCard = {
        id: uuid(),
        url: URL.createObjectURL(images[i]),
        size: images[i].size,
      };
      this.otherImages = [...this.otherImages, image];
    }
  }

  deleteProfileImage(imageId: string) {
    this.profileImage = {
      id: uuid(),
      url: 'assets/img/default.png',
      size: 0
    };
  }

  deleteOtherImage(imageId: string) {
    this.otherImages = this.otherImages.filter(x => x.id !== imageId);
  }

  onSubmit() {
    if (this.restaurantForm.invalid) {
      this.snackBar.open('Something went wrong.', 'Ok', {
        panelClass: 'warning-snackbar',
      });
      return;
    }

    this.snackBar.open("Successfully created restaurant", "Ok", { duration: 2000, panelClass: 'success-snackbar' });
  }
}
