import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { IRestaurantEdit } from 'src/app/_interfaces/IRestaurant';
import { RESTAURANT_FOR_EDIT } from 'src/app/fake_data/restaurant';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IImageCard } from 'src/app/_interfaces/IImage';
import { v4 as uuid } from 'uuid';

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
    ReactiveFormsModule
  ],
  templateUrl: './restaurants-edit.component.html',
  styleUrls: ['./restaurants-edit.component.css'],
})
export class RestaurantsEditComponent implements OnInit {
  restaurant: IRestaurantEdit | undefined;
  restaurantForm: FormGroup | undefined;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurant = RESTAURANT_FOR_EDIT;
    if (!this.restaurant) return;
    this.initForm(this.restaurant);
  }

  initForm(restaurantEdit: IRestaurantEdit) {
    this.restaurantForm = this.fb.group({
      name: [restaurantEdit.name, Validators.required],
      country: [restaurantEdit.country, Validators.required],
      postalCode: [restaurantEdit.postalCode, Validators.required],
      phone: [restaurantEdit.phone, Validators.required],
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
}
