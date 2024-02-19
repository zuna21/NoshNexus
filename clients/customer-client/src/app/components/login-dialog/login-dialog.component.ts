import { Component, OnDestroy } from '@angular/core';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { Subscription } from 'rxjs';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateModule } from '@ngx-translate/core';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-login-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule, 
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ReactiveFormsModule,
    TranslateModule,
    TitleCasePipe
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
  quicklySub?: Subscription;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private snackBar: MatSnackBar
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
    this.quicklySub = this.accountService.loginAsGuest().subscribe({
      next: user => {
        if (!user) this.dialogRef.close(false);
        else {
          this.dialogRef.close(true);
          this.snackBar.open("Thank you for creating an account.", "Ok");
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.loginSub?.unsubscribe();
    this.quicklySub?.unsubscribe();
  }
}
