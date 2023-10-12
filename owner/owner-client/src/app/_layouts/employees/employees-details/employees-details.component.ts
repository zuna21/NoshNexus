import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesDetailsHeaderComponent } from './employees-details-header/employees-details-header.component';
import { EmployeesDetailsOverviewComponent } from './employees-details-overview/employees-details-overview.component';
import { MatTabsModule } from '@angular/material/tabs';
import { IEmployeeDetails } from 'src/app/_interfaces/IEmployee';
import { EmployeeService } from 'src/app/_services/employee.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

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
export class EmployeesDetailsComponent implements OnInit, OnDestroy {
  employee: IEmployeeDetails | undefined;
  employeeId: string = '';

  employeeSub: Subscription | undefined;

  constructor(
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getEmployee();
  }

  getEmployee() {
    this.employeeId = this.activatedRoute.snapshot.params['id'];
    if (!this.employeeId) return;
    this.employeeSub = this.employeeService
      .getOwnerEmployeeDetails(this.employeeId)
      .subscribe({
        next: (employee) => (this.employee = employee),
      });
  }

  ngOnDestroy(): void {
    this.employeeSub?.unsubscribe();
  }
}
