import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  firstHide: boolean = true;
  secondHide: boolean = true;
  registerForm: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    repeatPassword: ['', [Validators.required, Validators.minLength(6)]]
  });

  constructor(
    private fb: FormBuilder
  ) {}

  checkPassword(event: any) {
    const value = event.target.value;
    if (this.registerForm.get('password')?.value !== value) {
      this.registerForm.get('repeatPassword')?.setErrors({'incorrect': true})
    } else this.registerForm.get('repeatPassword')?.setErrors(null);
  }

  onSubmit() {
    if (this.registerForm.invalid) return;
    console.log(this.registerForm.value);
  }
}
