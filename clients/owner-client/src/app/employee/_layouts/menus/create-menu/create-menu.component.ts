import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatSlideToggleModule} from '@angular/material/slide-toggle'; 
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MenuService } from 'src/app/employee/_services/menu.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-menu',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule, 
    MatInputModule,
    MatSlideToggleModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './create-menu.component.html',
  styleUrls: ['./create-menu.component.css']
})
export class CreateMenuComponent implements OnDestroy {
  menuForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    isActive: [true, Validators.required]
  })

  createMenuSub?: Subscription;

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService,
    private router: Router
  ) {}


  onSubmit() {
    if (this.menuForm.invalid) return;
    this.createMenuSub = this.menuService.create(this.menuForm.value).subscribe({
      next: _ => {
        this.router.navigateByUrl('/employee/menus');
      }
    })
  }


  ngOnDestroy(): void {
    this.createMenuSub?.unsubscribe();
  }
}
