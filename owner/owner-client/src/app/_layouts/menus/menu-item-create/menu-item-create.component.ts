import { Component, Input, OnDestroy, OnInit } from '@angular/core';
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
import { MenuService } from 'src/app/_services/menu.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

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
export class MenuItemCreateComponent implements OnDestroy {
  @Input('menuId') set setMenuId(value: string) {
    if (value != this.menuId) this.menuId = value;
  }
  menuItemForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    price: [null, Validators.required],
    description: [''],
    isActive: [true, Validators.required],
    hasSpecialOffer: [false, Validators.required],
    specialOfferPrice: [null],
  });
  menuItemImage: { id: string; url: string; size: number } = {
    id: uuid(),
    url: 'assets/img/default.png',
    size: 0,
  };
  menuId: string = '';

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService
  ) {}


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

  createMenuItemSub: Subscription | undefined;
  onSubmit() {
    if (this.menuItemForm.invalid || !this.menuId) return;
    this.createMenuItemSub = this.menuService.createMenuItem(this.menuId, this.menuItemForm.value).subscribe({
      next: _ => {
        this.menuItemForm.reset();
      }
    });
  }

  ngOnDestroy(): void {
    this.createMenuItemSub?.unsubscribe();
  }
}
