import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { IEditMenuItem, IGetMenuItemEdit } from 'src/app/_interfaces/IMenu';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuService } from 'src/app/_services/menu.service';
import { Subscription, mergeMap, of } from 'rxjs';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { v4 as uuid } from 'uuid';

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
  menuItem: IGetMenuItemEdit | undefined;
  menuItemId: string = '';
  menuItemProfileImage: { id: string; url: string; size: number } = {
    id: uuid(),
    url: 'http://localhost:5000/images/default/default.png',
    size: 0
  };
  menuItemProfileImageForm = new FormData();

  menuItemSub: Subscription | undefined;
  menuItemUpdateSub: Subscription | undefined;  
  menuItemImageSub: Subscription | undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    private menuService: MenuService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getMenuItem();
  }



  getMenuItem() {
    this.menuItemId = this.activatedRoute.snapshot.params['id'];
    if (!this.menuItemId) return;
    this.menuItemSub = this.menuService.getMenuItemEdit(this.menuItemId).subscribe({
      next: menuItem => {
        this.menuItem = menuItem;
        console.log(this.menuItem);
        this.initForm(this.menuItem);
      }
    });
  }

  initForm(menuItem: IGetMenuItemEdit) {
    this.menuItemForm = this.fb.group({
      name: [menuItem.name, Validators.required],
      price: [menuItem.price, Validators.required],
      description: [menuItem.description, Validators.required],
      isActive: [menuItem.isActive, Validators.required],
      hasSpecialOffer: [menuItem.hasSpecialOffer, Validators.required],
      specialOfferPrice: [menuItem.specialOfferPrice]
    });
    this.menuItemProfileImage = menuItem.profileImage ? menuItem.profileImage : {id: uuid(), url: 'http://localhost:5000/images/default/default.png', size: 0};
  }

  onSpecialOfferChange() {
    if (!this.menuItemForm) return;
    if (!this.menuItemForm.get('specialOffer')?.value) {
      this.menuItemForm.get('specialOfferPrice')?.patchValue(0);
    }
  }

  onAddImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.menuItemProfileImage = {
      id: uuid(),
      url: URL.createObjectURL(image),
      size: image.size
    };
    this.menuItemProfileImageForm.delete('image');
    this.menuItemProfileImageForm.append('image', image);
  }

  onDeleteProfileImage(imageId: string | number) {
    if (!this.menuItem) return;
    // ako je imageId isti kao menuItem.profileImage onda izbrisati sa servera
    //...
    if (imageId !== this.menuItem.profileImage.id) {
      this.menuItemProfileImage = {...this.menuItem.profileImage};
      this.menuItemProfileImageForm.delete('image');
    }
  }


  onSubmitProfileImage() {
    if (!this.menuItemProfileImageForm.has('image') || !this.menuItemId) return;
    this.menuItemImageSub = this.menuService
      .uploadMenuItemProfileImage(this.menuItemId, this.menuItemProfileImageForm)
      .subscribe({
        next: image => {
          if (!image || !this.menuItem) return;
          this.menuItem.profileImage = {...image, id: `${image.id}`};
          this.menuItemProfileImageForm.delete('image');
        }
      });
  }

  onSubmit() {
    if (!this.menuItemForm || this.menuItemForm.invalid || !this.menuItemForm.dirty) return;
    this.menuItemUpdateSub = this.menuService
      .updateMenuItem(this.menuItemId, this.menuItemForm.value)
      .subscribe({
        next: menuItemId => {
          if (!menuItemId) return;
          this.router.navigateByUrl(`/menus/menu-items/${menuItemId}`);
        }
      });
  }

  ngOnDestroy(): void {
    this.menuItemSub?.unsubscribe();
    this.menuItemUpdateSub?.unsubscribe();
    this.menuItemImageSub?.unsubscribe();
  }
}
