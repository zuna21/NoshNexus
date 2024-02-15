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

  accountSub?: Subscription;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder
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
      countryId: [account.countryId >= 0 ? account.countryId : null],
      city: [account.city],
      description: [account.description],
    });
  }

  onSubmit() {
    if (
      !this.accountForm ||
      this.accountForm.invalid ||
      !this.accountForm.dirty
    )
      return;
    console.log(this.accountForm.value);
  }

  ngOnDestroy(): void {
    this.accountSub?.unsubscribe();
  }
}
