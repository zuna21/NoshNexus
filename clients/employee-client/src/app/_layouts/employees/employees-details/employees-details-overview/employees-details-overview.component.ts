import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatDividerModule} from '@angular/material/divider'; 
import { MatIconModule } from '@angular/material/icon';
import { IEmployeeDetails } from 'src/app/_interfaces/IEmployee';

@Component({
  selector: 'app-employees-details-overview',
  standalone: true,
  imports: [CommonModule, MatDividerModule, MatIconModule],
  templateUrl: './employees-details-overview.component.html',
  styleUrls: ['./employees-details-overview.component.css']
})
export class EmployeesDetailsOverviewComponent {
  @Input('employee') employee: IEmployeeDetails | undefined;
}
