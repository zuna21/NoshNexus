import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { v4 as uuid } from 'uuid';

@Component({
  selector: 'app-menu-item-create',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    ImageWithDeleteComponent,
    MatButtonModule,
    MatIconModule,
    MatSlideToggleModule,
    ReactiveFormsModule,
  ],
  templateUrl: './menu-item-create.component.html',
  styleUrls: ['./menu-item-create.component.css'],
})
export class MenuItemCreateComponent {
  menuItemForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    price: [null, Validators.required],
    description: [''],
    active: [true, Validators.required],
    specialOffer: [false, Validators.required],
    specialOfferPrice: [null],
  });
  menuItemImage: { id: string; url: string; size: number } = {
    id: uuid(),
    url: 'assets/img/default.png',
    size: 0,
  };

  constructor(private fb: FormBuilder) {}

  onSpecialOfferChange() {
    if (!this.menuItemForm.get('specialOffer')?.value) {
      this.menuItemForm.get('specialOfferPrice')?.reset();
    }
  }

  onAddImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.menuItemImage = {
      id: uuid(),
      url: URL.createObjectURL(image),
      size: image.size,
    };
  }

  onDeleteImage() {
    this.menuItemImage = {
      id: uuid(),
      url: 'assets/img/default.png',
      size: 0,
    };
  }

  onSubmit() {
    if (this.menuItemForm.invalid) return;
    console.log(this.menuItemForm.value);
  }
}
