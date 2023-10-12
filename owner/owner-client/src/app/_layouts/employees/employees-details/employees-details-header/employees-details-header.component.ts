import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IEmployeeDetails } from 'src/app/_interfaces/IEmployee';

@Component({
  selector: 'app-employees-details-header',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './employees-details-header.component.html',
  styleUrls: ['./employees-details-header.component.css']
})
export class EmployeesDetailsHeaderComponent {
  @Input('employee') employee: IEmployeeDetails | undefined;

  isProfileLoading: boolean = true;
}
