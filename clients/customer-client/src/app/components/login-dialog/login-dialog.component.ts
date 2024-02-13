import { Component, OnDestroy } from '@angular/core';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { Subscription } from 'rxjs';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-login-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule, 
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login-dialog.component.html',
  styleUrl: './login-dialog.component.css',
})
export class LoginDialogComponent implements OnDestroy {
  isVisible: boolean = false;
  loginForm: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]]
  })

  loginSub?: Subscription;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    public dialogRef: MatDialogRef<LoginDialogComponent>,
  ) {}

  onLogin() {
    if (!this.loginForm.valid) return;
    this.loginSub = this.accountService.login(this.loginForm.value).subscribe({
      next: user => {
        if (!user) this.dialogRef.close(false);
        else this.dialogRef.close(true);
      }
    });
  }

  onQuicklyCreateAccount() {
    console.log('queckly create an account')
  }

  ngOnDestroy(): void {
    this.loginSub?.unsubscribe();
  }
}
