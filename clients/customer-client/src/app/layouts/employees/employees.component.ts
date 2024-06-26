import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { IEmployeeCard } from '../../interfaces/employee.interface';
import { Subscription } from 'rxjs';
import { EmployeeCardComponent } from '../../components/employee-card/employee-card.component';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    EmployeeCardComponent
  ],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent implements OnInit, OnDestroy {
  employees = signal<IEmployeeCard[]>([]);
  restaurantId?: number;

  employeeSub?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;
    this.employeeSub = this.employeeService.getEmployees(this.restaurantId).subscribe({
      next: employees => this.employees.set(employees)
    });
  }

  onViewEmployee(employeeId: number) {
    this.router.navigateByUrl(`/employees/${employeeId}`);
  }

  onViewRestaurant(restaurantId: number) {
    this.router.navigateByUrl(`/restaurants/${restaurantId}`);
  }

  ngOnDestroy(): void {
    this.employeeSub?.unsubscribe();
  }
}
