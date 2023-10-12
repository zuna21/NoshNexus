import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-employees-details-header',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './employees-details-header.component.html',
  styleUrls: ['./employees-details-header.component.css']
})
export class EmployeesDetailsHeaderComponent {
  isProfileLoading: boolean = true;
}
