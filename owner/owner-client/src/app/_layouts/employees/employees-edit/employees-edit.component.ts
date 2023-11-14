import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IGetEditEmployee } from 'src/app/_interfaces/IEmployee';
import { EmployeeService } from 'src/app/_services/employee.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-employees-edit',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    ImageWithDeleteComponent,
    ReactiveFormsModule
  ],
  templateUrl: './employees-edit.component.html',
  styleUrls: ['./employees-edit.component.css'],
})
export class EmployeesEditComponent implements OnInit, OnDestroy {
  hidePassword: boolean = true;
  employeeId: string = '';
  employee: IGetEditEmployee | undefined;
  employeeForm: FormGroup | undefined;

  employeeSub: Subscription | undefined;
  updateEmployeeSub: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getEmployee();
  }

  getEmployee() {
    this.employeeId = this.activatedRoute.snapshot.params['id'];
    if (!this.employeeId) return;
    this.employeeSub = this.employeeService.getEmployeeEdit(this.employeeId).subscribe({
      next: employee => {
        if (!employee) return;
        this.employee = employee;
        this.initForm(this.employee)
      }
    })
  }

  initForm(employee: IGetEditEmployee) {
    this.employeeForm = this.fb.group({
      firstName: [employee.firstName, Validators.required],
      lastName: [employee.lastName, Validators.required],
      email: [employee.email, [Validators.required, Validators.email]],
      username: [employee.username, Validators.required],
      password: [null],
      phoneNumber: [employee.phoneNumber, Validators.required],
      city: [employee.city, Validators.required],
      address: [employee.address, Validators.required],
      restaurantId: [employee.restaurantId, Validators.required],
      birth: [employee.birth, Validators.required],
      description: [employee.description],
      canEditMenus: [employee.canEditMenus, Validators.required],
      canViewFolders: [employee.canViewFolders, Validators.required],
      canEditFolders: [employee.canEditFolders, Validators.required]
    });
  }

  onSubmit() {
    if (!this.employeeForm || this.employeeForm.invalid || !this.employeeForm.dirty || !this.employeeId) return;
    this.updateEmployeeSub = this.employeeService.update(this.employeeId, this.employeeForm.value).subscribe({
      next: employeeId => {
        if(!employeeId) return;
        this.router.navigateByUrl(`/employees/${employeeId}`);
      }
    });
  }



  ngOnDestroy(): void {
    this.employeeSub?.unsubscribe();
    this.updateEmployeeSub?.unsubscribe();
  }
}
