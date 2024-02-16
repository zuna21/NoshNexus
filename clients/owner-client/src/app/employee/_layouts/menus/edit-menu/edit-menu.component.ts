import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MenuService } from 'src/app/employee/_services/menu.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IGetMenuEdit } from 'src/app/_interfaces/IMenu';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-edit-menu',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule, 
    MatInputModule,
    MatSlideToggleModule,
    MatButtonModule,
    ReactiveFormsModule,
    TranslateModule
  ],
  templateUrl: './edit-menu.component.html',
  styleUrls: ['./edit-menu.component.css']
})
export class EditMenuComponent implements OnInit, OnDestroy {
  menuForm?: FormGroup;
  menuId?: string;

  updateMenuSub?: Subscription;
  menuSub?: Subscription;

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}


  ngOnInit(): void {
    this.getMenu();
  }

  getMenu() {
    this.menuId = this.activatedRoute.snapshot.params['menuId'];
    if (!this.menuId) return;
    this.menuSub = this.menuService.getMenuEdit(this.menuId).subscribe({
      next: menu => {
        if (!menu) return;
        this.initForm(menu);
      }
    })
  }

  initForm(menu: IGetMenuEdit) {
    this.menuForm = this.fb.group({
      name: [menu.name, Validators.required],
      description: [menu.description],
      isActive: [menu.isActive, Validators.required]
    });
  }

  onSubmit() {
    if (!this.menuForm || this.menuForm.invalid || !this.menuForm.dirty || !this.menuId) return;
    this.updateMenuSub = this.menuService.update(this.menuId, this.menuForm.value).subscribe({
      next: _ => {
        this.router.navigateByUrl('/employee/menus');
      }
    })
  }


  ngOnDestroy(): void {
    this.updateMenuSub?.unsubscribe();
    this.menuSub?.unsubscribe();
  }
}
