import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-employee-card',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './employee-card.component.html',
  styleUrls: ['./employee-card.component.css']
})
export class EmployeeCardComponent {
  isLoadingBackgroundImage: boolean = true;
  isLoadingProfileImage: boolean = true;
}
