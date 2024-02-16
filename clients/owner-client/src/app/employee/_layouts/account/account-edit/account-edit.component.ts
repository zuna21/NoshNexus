import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Subscription } from 'rxjs';
import { IGetAccountEdit } from 'src/app/employee/_interfaces/account.interface';
import { AccountService } from 'src/app/_services/account.service';
import { Router } from '@angular/router';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { IImageCard } from 'src/app/_interfaces/IImage';
import { v4 as uuid } from 'uuid';
import { IUser } from 'src/app/_interfaces/IAccount';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-account-edit',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule, 
    MatInputModule, 
    MatSelectModule,
    ReactiveFormsModule,
    MatButtonModule,
    ImageWithDeleteComponent,
    MatIconModule,
    TranslateModule
  ],
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css']
})
export class AccountEditComponent implements OnInit, OnDestroy {
  editAccountForm?: FormGroup;
  account?: IGetAccountEdit;
  profileImage: IImageCard = {
    id: uuid(),
    size: 0,
    url: 'https://noshnexus.com/images/default/default-profile.png'
  }
  profileImageForm = new FormData();

  accountSub?: Subscription;
  updateAccountSub?: Subscription;
  uploadProfileImageSub?: Subscription;
  deleteImageSub?: Subscription;
  
  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {}
  
  
  ngOnInit(): void {
    this.getAccountEdit();
  }


  getAccountEdit() {
    this.accountSub = this.accountService.getEmployeeEdit().subscribe({
      next: account => {
        if (!account) return;
        this.account = account;
        this.initForm(this.account);
        if (this.account.profileImage) this.profileImage = {...this.account.profileImage};
      }
    });
  }

  initForm(account: IGetAccountEdit ) {
    this.editAccountForm = this.fb.group({
      username: [account.username, Validators.required],
      firstName: [account.firstName, Validators.required],
      lastName: [account.lastName, Validators.required],
      city: [account.city],
      countryId: [account.countryId],
      address: [account.address],
      description: [account.description],
      birth: [account.birth],
      phoneNumber: [account.phoneNumber],
      email: [account.email, Validators.email]
    })
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

  onSubmit() {
    if (!this.editAccountForm || this.editAccountForm.invalid || !this.editAccountForm.dirty) return;
    this.updateAccountSub = this.accountService.updateEmployee(this.editAccountForm.value).subscribe({
      next: _ => {
        this.router.navigateByUrl('/employee/account-details');
      }
    });
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
    this.updateAccountSub?.unsubscribe();
    this.uploadProfileImageSub?.unsubscribe();
    this.deleteImageSub?.unsubscribe();
  }
}

