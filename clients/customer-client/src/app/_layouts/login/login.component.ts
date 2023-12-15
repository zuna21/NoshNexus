import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {
  hide: boolean = true;
  loginForm: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });
  loginGuestSub?: Subscription;
  loginSub?: Subscription;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {}

  onSubmit() {
    if(!this.loginForm.valid) return;
    this.loginSub = this.accountService.login(this.loginForm.value).subscribe({
      next: _ => {
        this.router.navigateByUrl('/restaurants');
      }
    });
  }

  onLoginAsGuest() {
    this.loginGuestSub = this.accountService.loginAsGuest().subscribe({
      next: _ => {
        this.router.navigateByUrl('/restaurants');
      }
    })
  }

  ngOnDestroy(): void {
    this.loginGuestSub?.unsubscribe();
    this.loginSub?.unsubscribe();
  }
}
