import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IGetOwner } from 'src/app/_interfaces/IOwner';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-account-info',
  standalone: true,
  imports: [
    CommonModule, 
    MatDividerModule,
    MatButtonModule, 
    MatIconModule,
    TranslateModule
  ],
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css'],
})
export class AccountInfoComponent {
  @Input('account') account: IGetOwner | undefined;
}
