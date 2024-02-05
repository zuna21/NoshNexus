import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/employee/_services/account.service';
import { IGetAccountEdit } from 'src/app/employee/_interfaces/account.interface';

@Component({
  selector: 'app-account-edit',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule, 
    MatInputModule, 
    MatSelectModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css']
})
export class AccountEditComponent implements OnInit, OnDestroy {
  editAccountForm?: FormGroup;
  account?: IGetAccountEdit;

  accountSub?: Subscription;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService
  ) {}
  
  
  ngOnInit(): void {
    this.getAccountEdit();
  }


  getAccountEdit() {
    this.accountSub = this.accountService.getAccountEdit().subscribe({
      next: account => {
        if (!account) return;
        this.account = account;
        this.initForm(this.account);
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

  onSubmit() {
    if (!this.editAccountForm || this.editAccountForm.invalid || !this.editAccountForm.dirty) return;
    console.log(this.editAccountForm.value);
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
