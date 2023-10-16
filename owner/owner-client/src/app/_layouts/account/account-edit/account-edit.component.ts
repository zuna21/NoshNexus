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
import { IAccountEdit } from 'src/app/_interfaces/IAccount';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { COUNTRIES } from 'src/app/_shared/countries';

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
  account: IAccountEdit | undefined;
  accountForm: FormGroup | undefined;
  countries = COUNTRIES;
  profileImage: { id: string; url: string; size: number } = {
    id: '',
    url: 'assets/img/default-profile.png',
    size: 0,
  };

  accountSub: Subscription | undefined;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getAccount();
  }

  getAccount() {
    this.accountSub = this.accountService.getOwnerEdit().subscribe({
      next: (account) => {
        this.account = account;
        this.initForm(this.account);
        this.profileImage = this.account.profileImage;
      },
    });
  }

  initForm(account: IAccountEdit) {
    this.accountForm = this.fb.group({
      username: [account.username, Validators.required],
      firstName: [account.firstName, Validators.required],
      lastName: [account.lastName, Validators.required],
      email: [account.email, [Validators.required, Validators.email]],
      phone: [account.phone, Validators.required],
      birth: [account.birth, Validators.required],
      country: [account.country, Validators.required],
      city: [account.city, Validators.required],
      address: [account.address, Validators.required],
      description: [account.description, Validators.required],
    });
  }

  onProfileImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.profileImage = {
      id: '',
      url: URL.createObjectURL(image),
      size: image.size,
    };
  }

  onDeleteProfileImage() {
    this.profileImage = {
      id: '',
      url: 'assets/img/default-profile.png',
      size: 0,
    };
  }

  onSubmit() {
    if (!this.accountForm || this.accountForm.invalid) return;
    console.log(this.accountForm.value);
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
