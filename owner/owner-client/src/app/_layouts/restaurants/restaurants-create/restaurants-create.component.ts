import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { GalleryItem, ImageItem } from 'ng-gallery';
import { ICountry } from 'src/app/_interfaces/ICountry';
import { COUNTRIES } from 'src/app/fake_data/countries';

@Component({
  selector: 'app-restaurants-create',
  templateUrl: './restaurants-create.component.html',
  styleUrls: ['./restaurants-create.component.css']
})
export class RestaurantsCreateComponent implements OnInit {

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
    profilePicture: [null]
  })

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer, private fb: FormBuilder) {
    iconRegistry.addSvgIcon('facebook-logo', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/facebook-logo.svg'));
    iconRegistry.addSvgIcon('instagram-logo', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/instagram-logo.svg'));
    iconRegistry.addSvgIcon('globe', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/globe.svg'));
    iconRegistry.addSvgIcon('image', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/image.svg'));
    iconRegistry.addSvgIcon('images', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/images.svg'));
    iconRegistry.addSvgIcon('save', sanitizer.bypassSecurityTrustResourceUrl('assets/svg/save.svg'));
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
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
      new ImageItem({src: 'assets/img/default.png', thumb: 'assets/img/default.png'}),
    ];
  }


  imageURL: string = '';
  showPreview(event: any) {
    const fileHTML = event.target as HTMLInputElement;
    if (!fileHTML) return;
    if (!fileHTML.files) return;
    const file = fileHTML.files[0];
    this.restaurantFormGroup.patchValue({
      profilePicture: file
    });

    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    }
    reader.readAsDataURL(file)
    console.log(this.imageURL);
  }

  onSubmit() {
    if (this.restaurantFormGroup.invalid) return;
    console.log(this.restaurantFormGroup.value);
  }

}
