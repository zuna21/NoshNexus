import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-order-decline-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule, 
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './order-decline-dialog.component.html',
  styleUrls: ['./order-decline-dialog.component.css']
})
export class OrderDeclineDialogComponent {
  declineForm: FormGroup = this.fb.group({
    reason: ['', Validators.required]
  });

  constructor(
    public dialogRef: MatDialogRef<OrderDeclineDialogComponent>,
    private fb: FormBuilder
  ) {}

  onClose() {
    this.dialogRef.close();
  }

  onDecline() {
    this.dialogRef.close(this.declineForm.value);
  }
}
