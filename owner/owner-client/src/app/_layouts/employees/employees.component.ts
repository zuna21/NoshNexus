import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeCardComponent } from 'src/app/_components/employee-card/employee-card.component';
import { IEmployeeCard } from 'src/app/_interfaces/IEmployee';
import { Subscription } from 'rxjs';
import { EmployeeService } from 'src/app/_services/employee.service';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, EmployeeCardComponent],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
})
export class EmployeesComponent implements OnInit, OnDestroy {
  employeesCards: IEmployeeCard[] = [];

  employeesCardSub: Subscription | undefined;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.getEmployeesCards();
  }

  getEmployeesCards() {
    this.employeesCardSub = this.employeeService
      .getOwnerEmployeesCards()
      .subscribe({
        next: (employeesCards) => (this.employeesCards = employeesCards),
      });
  }

  ngOnDestroy(): void {
    this.employeesCardSub?.unsubscribe();
  }
}
