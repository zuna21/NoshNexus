import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IEmployeeCard } from 'src/app/_interfaces/IEmployee';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-employee-card',
  standalone: true,
  imports: [
    CommonModule, 
    MatButtonModule, 
    MatProgressSpinnerModule, 
    RouterLink, 
    MatCardModule, 
    MatProgressSpinnerModule,
    TranslateModule
  ],
  templateUrl: './employee-card.component.html',
  styleUrls: ['./employee-card.component.css']
})
export class EmployeeCardComponent {
  @Input('employee') employee: IEmployeeCard | undefined;

  isLoadingBackgroundImage: boolean = true;
  isLoadingProfileImage: boolean = true;
}
