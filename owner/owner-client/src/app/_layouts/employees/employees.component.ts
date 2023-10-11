import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeCardComponent } from 'src/app/_components/employee-card/employee-card.component';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, EmployeeCardComponent],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent {

}
