import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileHeaderComponent } from 'src/app/_components/profile-header/profile-header.component';
import {MatTabsModule} from '@angular/material/tabs'; 
import { AccountInfoComponent } from './account-info/account-info.component';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    CommonModule,
    ProfileHeaderComponent,
    MatTabsModule,
    AccountInfoComponent
  ],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {

}
