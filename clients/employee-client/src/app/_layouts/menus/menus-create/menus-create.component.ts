import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { IRestaurantSelect } from 'src/app/_interfaces/IRestaurant';
import {MatSlideToggleModule} from '@angular/material/slide-toggle'; 
import { Subscription } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MenuService } from 'src/app/_services/menu.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menus-create',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatSlideToggleModule
  ],
  templateUrl: './menus-create.component.html',
  styleUrls: ['./menus-create.component.css'],
})
export class MenusCreateComponent implements OnDestroy {
  restaurants: IRestaurantSelect[] = [];
  menuForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    restaurantId: [-1], // Ovo ce se svakako na backendu dodjeljivati
    isActive: [false, Validators.required]
  });

  menuCreateSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService,
    private router: Router
  ) {}


  onSubmit() {
    if (!this.menuForm.valid) return;
    this.menuCreateSub = this.menuService.create(this.menuForm.value).subscribe({
      next: menuId => {
        this.router.navigateByUrl(`/menus/${menuId}`);
      }
    });
  }

  ngOnDestroy(): void {
    this.menuCreateSub?.unsubscribe();
  }


}
