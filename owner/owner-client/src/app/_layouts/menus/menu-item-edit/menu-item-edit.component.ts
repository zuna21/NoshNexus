import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { IMenuItemEdit } from 'src/app/_interfaces/IMenu';
import { ActivatedRoute } from '@angular/router';
import { MenuService } from 'src/app/_services/menu.service';
import { Subscription } from 'rxjs';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-menu-item-edit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    ImageWithDeleteComponent,
    MatIconModule,
    MatButtonModule,
    MatSlideToggleModule
  ],
  templateUrl: './menu-item-edit.component.html',
  styleUrls: ['./menu-item-edit.component.css']
})
export class MenuItemEditComponent implements OnInit, OnDestroy {
  menuItemForm: FormGroup | undefined;
  menuItem: IMenuItemEdit | undefined;
  menuItemId: string = '';
  menuItemImage: { id: string; url: string; size: number } = {
    id: '',
    url: 'assets/img/default.png',
    size: 0
  };

  menuItemSub: Subscription | undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    private menuService: MenuService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.getMenuItem();
  }

  onDeleteImage() {
    this.menuItemImage = {
      id: '',
      url: 'assets/img/default.png',
      size: 0
    }
  }

  getMenuItem() {
    this.menuItemId = this.activatedRoute.snapshot.params['id'];
    if (!this.menuItemId) return;
    this.menuItemSub = this.menuService.getOwnerMenuItemEdit(this.menuItemId).subscribe({
      next: menuItem => {
        this.menuItem = menuItem;
        this.initForm(this.menuItem);
      }
    });
  }

  initForm(menuItem: IMenuItemEdit) {
    this.menuItemForm = this.fb.group({
      name: [menuItem.name, Validators.required],
      price: [menuItem.price, Validators.required],
      description: [menuItem.description, Validators.required],
      active: [menuItem.active, Validators.required],
      specialOffer: [menuItem.specialOffer, Validators.required],
      specialOfferPrice: [menuItem.specialOfferPrice]
    });

    this.menuItemImage = menuItem.image;
  }

  onSpecialOfferChange() {
    if (!this.menuItemForm) return;
    if (!this.menuItemForm.get('specialOffer')?.value) {
      this.menuItemForm.get('specialOfferPrice')?.reset();
    }
  }

  onAddImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.menuItemImage = {
      id: '',
      url: URL.createObjectURL(image),
      size: image.size
    };
  }

  onSubmit() {
    if (!this.menuItemForm || this.menuItemForm.invalid) return;
    console.log(this.menuItemForm.value);
  }

  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
  }
}
