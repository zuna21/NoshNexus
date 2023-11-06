import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { IRestaurantEdit } from 'src/app/_interfaces/IRestaurant';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IImageCard } from 'src/app/_interfaces/IImage';
import { v4 as uuid } from 'uuid';
import {MatSelectModule} from '@angular/material/select'; 
import { ICountry } from 'src/app/_interfaces/ICountry';
import { COUNTRIES } from 'src/app/_shared/countries';
import { RestaurantService } from 'src/app/_services/restaurant.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-restaurants-edit',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatSlideToggleModule,
    MatChipsModule,
    MatButtonModule,
    ImageWithDeleteComponent,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './restaurants-edit.component.html',
  styleUrls: ['./restaurants-edit.component.css'],
})
export class RestaurantsEditComponent implements OnInit, OnDestroy {
  restaurant: IRestaurantEdit | undefined;
  restaurantForm: FormGroup | undefined;
  countries: ICountry[] = [];
  restaurantId: string = '';

  restaurantSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private restaurantService: RestaurantService,
    private activatedRoute: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.getRestaurant();
    this.getCountries();
  }

  getRestaurant() {
    this.restaurantId = this.activatedRoute.snapshot.params['id'];
    if (!this.restaurantId) return;
    this.restaurantSub = this.restaurantService.getOwnerRestaurantEdit(this.restaurantId).subscribe({
      next: restaurant => {
        if (!restaurant) return;
        this.restaurant = restaurant;
        this.initForm(this.restaurant);
      }
    })
  }

  getCountries() {
    this.countries = COUNTRIES;
  }

  initForm(restaurantEdit: IRestaurantEdit) {
    this.restaurantForm = this.fb.group({
      name: [restaurantEdit.name, Validators.required],
      country: [restaurantEdit.country, Validators.required],
      postalCode: [restaurantEdit.postalCode, Validators.required],
      phone: [restaurantEdit.phoneNumber, Validators.required],
      city: [restaurantEdit.city, Validators.required],
      address: [restaurantEdit.address, Validators.required],
      facebookUrl: [restaurantEdit.facebookUrl],
      instagramUrl: [restaurantEdit.instagramUrl],
      websiteUrl: [restaurantEdit.websiteUrl],
      description: [restaurantEdit.description],
      isActive: [restaurantEdit.isActive, Validators.required]
    });
  }

  onUploadProfilePhoto(event: any) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const file = inputHTML.files[0];
    const imageForCard: IImageCard = {
      id: uuid(),
      url: URL.createObjectURL(file),
      size: file.size
    }
    if (!this.restaurant) return;
    this.restaurant.profileImage = imageForCard;
  }

  onUploadRestaurantImages(event: any) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const files = inputHTML.files;
    if (!this.restaurant) return;
    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      const imageToArray: IImageCard = {
        id: uuid(),
        url: URL.createObjectURL(file),
        size: file.size
      };
      this.restaurant.images = [...this.restaurant.images, imageToArray];
    }
  }

  onDeleteImage(imageId: string) {
    if (!this.restaurant) return;
    this.restaurant.images = this.restaurant.images.filter(x => x.id !== imageId);
  }

  onDeleteProfileImage(imageId: string) {
    if (!this.restaurant) return;
    const defaultImage: IImageCard = {
      id: uuid(),
      url: 'assets/img/default.png',
      size: 0
    }

    this.restaurant.profileImage = defaultImage;
  }

  onSubmit() {
    if (!this.restaurantForm || this.restaurantForm.invalid) return;
    console.log(this.restaurantForm.value);
  }

  ngOnDestroy(): void {
      this.restaurantSub?.unsubscribe();
  }
}
