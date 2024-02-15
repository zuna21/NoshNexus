import { Component, OnDestroy, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-activate-account-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule, 
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule
  ],
  templateUrl: './activate-account-dialog.component.html',
  styleUrl: './activate-account-dialog.component.css'
})
export class ActivateAccountDialogComponent implements OnDestroy {
  activateFrom: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    repeatPassword: ['', [Validators.required, Validators.minLength(6)]]
  });
  showPassword = signal<boolean>(false);
  showRepeatPassword = signal<boolean>(false);

  activateSub?: Subscription;


  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private accountService: AccountService,
    public dialogRef: MatDialogRef<ActivateAccountDialogComponent>,
  ) {}

  checkPasswords(): boolean {
    return this.activateFrom.get('password')?.value === this.activateFrom.get('repeatPassword')?.value;
  }

  onSubmit() {
    if (this.activateFrom.invalid) return;
    if (!this.checkPasswords()) {
      this.snackBar.open("Passwords doesn't match.", "Ok");
      this.activateFrom.get('password')?.reset();
      this.activateFrom.get('repeatPassword')?.reset();
      return;
    } else {
      this.activateSub = this.accountService.activateAccount(this.activateFrom.value).subscribe({
        next: account => {
          if (!account) {
            this.dialogRef.close({isActivated: false, username: null});
            return;
          } else {
            this.dialogRef.close({isActivated: true, username: this.activateFrom.get('username')?.value})
          }
        }
      })
    }
  }


  ngOnDestroy(): void {
    this.activateSub?.unsubscribe();
  }

  
}
