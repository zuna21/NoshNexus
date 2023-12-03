import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IGetMenuEdit } from 'src/app/_interfaces/IMenu';
import { MenuService } from 'src/app/_services/menu.service';
import {MatSlideToggleModule} from '@angular/material/slide-toggle'; 

@Component({
  selector: 'app-menus-edit',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatIconModule,
    MatSlideToggleModule
  ],
  templateUrl: './menus-edit.component.html',
  styleUrls: ['./menus-edit.component.css']
})
export class MenusEditComponent implements OnInit, OnDestroy {
  menuForm: FormGroup | undefined;
  menuId: string = '';
  menu: IGetMenuEdit | undefined

  menuSub: Subscription | undefined;
  menuUpdateSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private menuService: MenuService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getMenu();
  }

  getMenu() {
    this.menuId = this.activatedRoute.snapshot.params['id'];
    if (!this.menuId) return;
    this.menuSub = this.menuService.getMenuEdit(this.menuId).subscribe({
      next: menu => {
        this.menu = menu;
        this.initForm(this.menu);
      }
    });
  }

  initForm(menu: IGetMenuEdit) {
    this.menuForm = this.fb.group({
      name: [menu.name, Validators.required],
      description: [menu.description],
      isActive: [menu.isActive, Validators.required]
    });
  }

  onSubmit() {
    if (!this.menuForm || this.menuForm.invalid || !this.menuId) return;
    this.menuUpdateSub = this.menuService.update(this.menuId, this.menuForm.value).subscribe({
      next: menuId => {
        this.router.navigateByUrl(`/menus/${menuId}`);
      }
    });

  }

  ngOnDestroy(): void {
    this.menuSub?.unsubscribe();
    this.menuUpdateSub?.unsubscribe();
  }
}
