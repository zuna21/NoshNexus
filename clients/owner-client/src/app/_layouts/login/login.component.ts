import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  hide: boolean = true;
  loginForm: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });
  loginSub: Subscription | undefined;
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router
    
  ) {}

  onSubmit() {
    if (this.loginForm.invalid) return;
    this.isLoading = true;
    this.loginSub = this.accountService.login(this.loginForm.value).subscribe({
      next: user => {
        this.isLoading = false;
        if (!user.token) return;
        const role = JSON.parse(JSON.stringify(jwtDecode(user.token))).role;
        if (role === "owner")
          this.router.navigateByUrl('/home');
        else 
          this.router.navigateByUrl('/employee/home');
      },
      error: _ => {
        this.isLoading = false;
      }
    });
  }
}
