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
import { IImageCard } from 'src/app/_interfaces/IImage';
import { v4 as uuid } from 'uuid';
import { IUser } from 'src/app/_interfaces/IAccount';
import { TranslateModule } from '@ngx-translate/core';

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
    TranslateModule
  ],
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css'],
})
export class AccountEditComponent implements OnInit, OnDestroy {
  account: IGetOwnerEdit | undefined;
  accountForm: FormGroup | undefined;
  profileImage: IImageCard = {
    id: uuid(),
    size: 0,
    url: 'https://noshnexus.com/images/default/default-profile.png'
  }
  profileImageForm = new FormData();
  
  accountSub: Subscription | undefined;
  updateOwnerSub: Subscription | undefined;
  uploadProfileImageSub: Subscription | undefined;
  deleteImageSub?: Subscription;
  
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
        if (this.account.profileImage) this.profileImage = {...this.account.profileImage};
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

  onProfileImage(event: Event) {
    const inputHTML = event.target as HTMLInputElement;
    if (!inputHTML || !inputHTML.files || inputHTML.files.length <= 0) return;
    const image = inputHTML.files[0];
    this.profileImage = {
      id: '',
      url: URL.createObjectURL(image),
      size: image.size,
    };
    this.profileImageForm.delete('image');
    this.profileImageForm.append('image', image);
  }


  onSubmitProfileImage() {
    if (!this.profileImageForm.has('image')) return;
    this.uploadProfileImageSub = this.accountService.uploadProfileImage(this.profileImageForm)
      .subscribe({
        next: uploadedImage => {
          if (!uploadedImage || !this.account) return;
          this.profileImage = {...uploadedImage};
          this.account.profileImage = {...this.profileImage};
          const currentAccount = this.accountService.getUserSubject();
          if (currentAccount) {
            const updatedAccount : IUser = {
              ...currentAccount,
              profileImage: this.profileImage.url
            }
            this.accountService.setUser(updatedAccount);
          }
          this.profileImageForm.delete('image');
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
    this.updateOwnerSub = this.accountService
      .update(this.accountForm.value)
      .subscribe({
        next: (owner) => {
          if (!owner) return;
          this.router.navigateByUrl(`/account`);
        },
      });
  }

  deleteProfileImage(imageId: string | number) {
    if (this.profileImage.size === 0) return;
    this.deleteImageSub = this.accountService.deleteImage(imageId)
      .subscribe({
        next: _ => {
          this.profileImage = {id: uuid(), url: 'https://noshnexus.com/images/default/default-profile.png', size: 0};
          const currentAccount = this.accountService.getUserSubject();
          if (currentAccount) {
            const updatedAccount : IUser = {
              ...currentAccount,
              profileImage: this.profileImage.url
            }
            this.accountService.setUser(updatedAccount);
          }
          this.profileImageForm.delete('image')
        }
      });
  }


  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
    this.updateOwnerSub?.unsubscribe();
    this.uploadProfileImageSub?.unsubscribe();
    this.deleteImageSub?.unsubscribe();
  }
}
