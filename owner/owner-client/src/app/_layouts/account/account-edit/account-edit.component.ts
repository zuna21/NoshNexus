import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { IGetOwnerEdit } from 'src/app/_interfaces/IOwner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-edit',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ImageWithDeleteComponent,
    ReactiveFormsModule,
  ],
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css'],
})
export class AccountEditComponent implements OnInit, OnDestroy {
  account: IGetOwnerEdit | undefined;
  accountForm: FormGroup | undefined;
  
  accountSub: Subscription | undefined;
  updateOwnerSub: Subscription | undefined;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAccount();
  }

  getAccount() {
    this.accountSub = this.accountService.getOwnerEdit().subscribe({
      next: (account) => {
        this.account = account;
        this.initForm(this.account);
      },
    });
  }

  initForm(account: IGetOwnerEdit) {
    this.accountForm = this.fb.group({
      username: [account.username, Validators.required],
      firstName: [account.firstName, Validators.required],
      lastName: [account.lastName, Validators.required],
      email: [account.email, [Validators.required, Validators.email]],
      phoneNumber: [account.phoneNumber, Validators.required],
      birth: [account.birth, Validators.required],
      countryId: [account.countryId, Validators.required],
      city: [account.city, Validators.required],
      address: [account.address, Validators.required],
      description: [account.description],
    });
  }

  /* onProfileImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.profileImage = {
      id: '',
      url: URL.createObjectURL(image),
      size: image.size,
    };
  } */

  onSubmit() {
    if (
      !this.accountForm ||
      this.accountForm.invalid ||
      !this.accountForm.dirty
    )
      return;
    this.updateOwnerSub = this.accountService
      .update(this.accountForm.value)
      .subscribe({
        next: (ownerId) => {
          if (!ownerId) return;
          this.router.navigateByUrl(`/account`);
        },
      });
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
    this.updateOwnerSub?.unsubscribe();
  }
}
