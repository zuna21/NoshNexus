import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { IEmployee } from '../../../interfaces/employee.interface';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../../services/employee.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-details',
  standalone: true,
  imports: [
    MatProgressSpinnerModule,
    MatDividerModule,
    DatePipe
  ],
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css',
})
export class EmployeeDetailsComponent implements OnInit, OnDestroy {
  employee?: IEmployee;
  employeeId?: number;
  isBackgroundImgLoading = signal<boolean>(true);

  employeeSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private employeeService: EmployeeService
  ) {}

  ngOnInit(): void {
    this.getEmployee();
  }

  getEmployee() {
    this.employeeId = parseInt(
      this.activatedRoute.snapshot.params['employeeId']
    );
    if (!this.employeeId) return;
    this.employeeSub = this.employeeService
      .getEmployee(this.employeeId)
      .subscribe({
        next: (employee) => (this.employee = employee),
      });
  }

  ngOnDestroy(): void {
    this.employeeSub?.unsubscribe();
  }
}
