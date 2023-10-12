import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesDetailsHeaderComponent } from './employees-details-header/employees-details-header.component';
import { EmployeesDetailsOverviewComponent } from './employees-details-overview/employees-details-overview.component';
import { MatTabsModule } from '@angular/material/tabs';

@Component({
  selector: 'app-employees-details',
  standalone: true,
  imports: [
    CommonModule,
    EmployeesDetailsHeaderComponent,
    MatTabsModule,
    EmployeesDetailsOverviewComponent,
  ],
  templateUrl: './employees-details.component.html',
  styleUrls: ['./employees-details.component.css'],
})
export class EmployeesDetailsComponent {}
