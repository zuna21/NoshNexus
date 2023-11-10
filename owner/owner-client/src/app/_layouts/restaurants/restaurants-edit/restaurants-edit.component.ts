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
  restaurantId: string = '';
  otherImages: IImageCard[] = [];
  profileImageForm = new FormData();
  otherImagesForm = new FormData();

  restaurantSub: Subscription | undefined;
  profileImageSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private restaurantService: RestaurantService,
    private activatedRoute: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.getRestaurant();
  }

  getRestaurant() {
    this.restaurantId = this.activatedRoute.snapshot.params['id'];
    if (!this.restaurantId) return;
    this.restaurantSub = this.restaurantService.getRestaurantEdit(this.restaurantId).subscribe({
      next: restaurant => {
        if (!restaurant) return;
        this.restaurant = restaurant;
        this.initForm(this.restaurant);
      }
    })
  }


  initForm(restaurantEdit: IRestaurantEdit) {
    this.restaurantForm = this.fb.group({
      name: [restaurantEdit.name, Validators.required],
      countryId: [restaurantEdit.countryId, Validators.required],
      currencyId: [restaurantEdit.currencyId, Validators.required],
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
    this.profileImageForm.delete('image');
    this.profileImageForm.append('image', file);
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
      this.otherImages = [...this.otherImages, imageToArray];
      this.otherImagesForm.append('image', file);
    }
  }



  onSubmitProfileImage() {
    if (!this.profileImageForm.has('image')) return;
    this.profileImageSub = this.restaurantService.uploadProfileImage(this.restaurantId, this.profileImageForm).subscribe({
      next: profileImage => {
        if (!this.restaurant) return;
        this.restaurant.profileImage = {
          ...profileImage,
          url: `http://localhost:5000/${profileImage.url}`
        };
        this.profileImageForm.delete('image');
      }
    });
  }

  onSubmitOtherImages() {
    if (!this.otherImagesForm.has('image')) return;
    console.log(this.otherImagesForm);
  }

  onSubmit() {
    if (!this.restaurantForm || this.restaurantForm.invalid || !this.restaurantForm.dirty) return;
    console.log(this.restaurantForm.value);
  }

  ngOnDestroy(): void {
      this.restaurantSub?.unsubscribe();
      this.profileImageSub?.unsubscribe();
  }
}
