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
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { IMenuItemCard } from 'src/app/_interfaces/IMenu';

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
  
  menuId: string = '';

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService
  ) {}


  onSpecialOfferChange() {
    if (!this.menuItemForm.get('specialOffer')?.value) {
      this.menuItemForm.get('specialOfferPrice')?.patchValue(0);
    }
  }


  createMenuItemSub: Subscription | undefined;
  onSubmit() {
    if (this.menuItemForm.invalid || !this.menuId) return;
    this.createMenuItemSub = this.menuService.createMenuItem(this.menuId, this.menuItemForm.value).subscribe({
      next: menuItem => {
        this.menuItemForm.reset();
        this.menuItemCreated.emit(menuItem);
      }
    });
  }

  ngOnDestroy(): void {
    this.createMenuItemSub?.unsubscribe();
  }
}
