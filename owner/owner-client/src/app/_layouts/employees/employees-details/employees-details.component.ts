import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesDetailsOverviewComponent } from './employees-details-overview/employees-details-overview.component';
import { MatTabsModule } from '@angular/material/tabs';
import { IEmployeeDetails } from 'src/app/_interfaces/IEmployee';
import { EmployeeService } from 'src/app/_services/employee.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountHeaderComponent } from 'src/app/_components/account-header/account-header.component';

@Component({
  selector: 'app-employees-details',
  standalone: true,
  imports: [
    CommonModule,
    MatTabsModule,
    EmployeesDetailsOverviewComponent,
    AccountHeaderComponent
  ],
  templateUrl: './employees-details.component.html',
  styleUrls: ['./employees-details.component.css'],
})
export class EmployeesDetailsComponent implements OnInit, OnDestroy {
  employee: IEmployeeDetails | undefined;
  employeeId: string = '';

  employeeSub: Subscription | undefined;
  employeeDeleteSub: Subscription | undefined;

  constructor(
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getEmployee();
  }

  onDelete(canDelete: boolean) {
    if (!this.employee || !canDelete) return;
    this.employeeDeleteSub = this.employeeService.delete(this.employee.id).subscribe({
      next: _ => {
        this.router.navigateByUrl(`/employees`);
      }
    });
  }

  onEdit(canEdit: boolean) {
    if (!canEdit || !this.employee) return;
    this.router.navigateByUrl(`/employees/edit/${this.employee.id}`);
  }

  getEmployee() {
    this.employeeId = this.activatedRoute.snapshot.params['id'];
    if (!this.employeeId) return;
    this.employeeSub = this.employeeService
      .getEmployee(this.employeeId)
      .subscribe({
        next: (employee) => {
          this.employee = employee;
        },
      });
  }

  ngOnDestroy(): void {
    this.employeeSub?.unsubscribe();
    this.employeeDeleteSub?.unsubscribe();
  }
}
