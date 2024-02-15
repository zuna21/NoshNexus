import { Component, OnDestroy, OnInit } from '@angular/core';
import { IGetAccountEdit } from '../../../interfaces/account.interface';
import { AccountService } from '../../../services/account.service';
import { Subscription } from 'rxjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { IImage } from '../../../interfaces/image.interface';

@Component({
  selector: 'app-edit-account',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './edit-account.component.html',
  styleUrl: './edit-account.component.css',
})
export class EditAccountComponent implements OnInit, OnDestroy {
  account?: IGetAccountEdit;
  accountForm?: FormGroup;
  profileImageForm = new FormData();
  profileImage: IImage = {
    id: -1,
    url: "https://noshnexus.com/images/default/default-profile.png"
  };

  accountSub?: Subscription;
  editAccountSub?: Subscription;
  profileImageSub?: Subscription;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAccountEdit();
  }

  getAccountEdit() {
    this.accountSub = this.accountService.getAccountEdit().subscribe({
      next: (account) => {
        if (!account) return;
        this.account = account;
        this.initForm(this.account);
      },
    });
  }

  initForm(account: IGetAccountEdit) {
    this.accountForm = this.fb.group({
      username: [account.username, Validators.required],
      firstName: [account.firstName],
      lastName: [account.lastName],
      countryId: [account.countryId],
      city: [account.city],
      description: [account.description],
    });

    if (!account.profileImage) return;
    this.profileImage = {...account.profileImage};
  }

  onUploadImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.profileImage = {
      id: -1,
      url: URL.createObjectURL(image),
    };
    this.profileImageForm.delete('image');
    this.profileImageForm.append('image', image);
  }

  onRemoveProfileImage() {
    if (!this.profileImageForm.has('image')) return;
    this.profileImageForm.delete('image');
    this.profileImage = {
      id: -1,
      url: 'https://noshnexus.com/images/default/default-profile.png'
    };
  }

  onSubmitProfileImage() {
    if (!this.profileImageForm.has('image')) return;
    this.profileImageSub = this.accountService.uploadProfileImage(this.profileImageForm).subscribe({
      next: image => {
        if (!image) return;
        this.profileImageForm.delete('image');
        this.profileImage = {
          id: image.id,
          url: image.url
        };
        this.snackBar.open("Successfully changed profile image.", "Ok");
      }
    })
  }


  onSubmit() {
    if (
      !this.accountForm ||
      this.accountForm.invalid ||
      !this.accountForm.dirty
    )
      return;
    console.log(this.accountForm.value);
    this.editAccountSub = this.accountService.editAccount(this.accountForm.value).subscribe({
      next: user => {
        if (!user) return;
        this.router.navigateByUrl('/account');
        this.snackBar.open("Successfully updated account.", "Ok");
      }
    });
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
    this.editAccountSub?.unsubscribe();
    this.profileImageSub?.unsubscribe();
  }
}
