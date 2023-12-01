import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
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
import { Subscription, mergeMap, of } from 'rxjs';
import { IMenuItemCard } from 'src/app/_interfaces/IMenu';
import { IImageCard } from 'src/app/_interfaces/IImage';

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
  @Output('menuItemCreated') menuItemCreated = new EventEmitter<IMenuItemCard>();

  menuItemForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    price: [null, Validators.required],
    description: [''],
    isActive: [true, Validators.required],
    hasSpecialOffer: [false, Validators.required],
    specialOfferPrice: [0],
  });
  menuItemProfileImage: IImageCard = {
    id: uuid(),
    url: 'http://localhost:5000/images/default/default.png',
    size: 0
  };
  menuId: string = '';
  menuItemImageForm = new FormData();
  menuItemCard: IMenuItemCard | undefined;


  createMenuItemSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService
  ) {}


  onSpecialOfferChange() {
    if (!this.menuItemForm.get('specialOffer')?.value) {
      this.menuItemForm.get('specialOfferPrice')?.patchValue(0);
    }
  }

  onUploadImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const file = inputHTML.files[0];
    const imageForCard: IImageCard = {
      id: uuid(),
      url: URL.createObjectURL(file),
      size: file.size,
    };
    this.menuItemImageForm.delete('image');
    this.menuItemImageForm.append('image', file);
    this.menuItemProfileImage = {...imageForCard};
  }

  onDeleteImage() {
    this.menuItemProfileImage = {
      id: uuid(),
      url: 'http://localhost:5000/images/default/default.png',
      size: 0
    };
    this.menuItemImageForm.delete('image');
  }

  onSubmit() {
    if (this.menuItemForm.invalid || !this.menuId) return;

    // Prvo se sacuva menuItem pa onda slika
    this.createMenuItemSub = this.menuService.createMenuItem(this.menuId, this.menuItemForm.value).pipe(
      mergeMap(menuItem => {
        if (!menuItem) return of(null);
        this.menuItemCard = {...menuItem, image: ''};
        if (!this.menuItemImageForm.has('image')) return of(null);
        return this.menuService.uploadMenuItemProfileImage(`${menuItem.id}`, this.menuItemImageForm)
      })
    ).subscribe({
      next: profileImage => {
        if (!this.menuItemCard) return;
        this.menuItemCard = {...this.menuItemCard, image: profileImage ? profileImage.url : ''};
        this.menuItemCreated.emit(this.menuItemCard);
        this.resetForm();
      }
    });
  }

  resetForm() {
    this.menuItemForm.reset();
    this.menuItemForm.get('hasSpecialOffer')?.patchValue(false);
    this.menuItemForm.get('isActive')?.patchValue(true);
    this.menuItemForm.get('specialOfferPrice')?.patchValue(0);
    this.menuItemImageForm.delete('image');
    this.menuItemProfileImage = {
      id: uuid(),
      size: 0,
      url: 'http://localhost:5000/images/default/default.png'
    };
    this.menuItemCard = undefined;
  }

  ngOnDestroy(): void {
    this.createMenuItemSub?.unsubscribe();
  }
}
