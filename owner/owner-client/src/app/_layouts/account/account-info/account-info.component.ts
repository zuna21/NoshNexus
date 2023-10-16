import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IAccount } from 'src/app/_interfaces/IAccount';

@Component({
  selector: 'app-account-info',
  standalone: true,
  imports: [CommonModule, MatDividerModule, MatButtonModule, MatIconModule],
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css'],
})
export class AccountInfoComponent {
  @Input('account') account: IAccount | undefined;
}
