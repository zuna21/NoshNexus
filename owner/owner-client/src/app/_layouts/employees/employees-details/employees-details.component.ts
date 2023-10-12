import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesDetailsHeaderComponent } from './employees-details-header/employees-details-header.component';

@Component({
  selector: 'app-employees-details',
  standalone: true,
  imports: [CommonModule, EmployeesDetailsHeaderComponent],
  templateUrl: './employees-details.component.html',
  styleUrls: ['./employees-details.component.css']
})
export class EmployeesDetailsComponent {

}
