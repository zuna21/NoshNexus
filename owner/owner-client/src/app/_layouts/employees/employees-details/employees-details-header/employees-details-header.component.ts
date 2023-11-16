import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IEmployeeDetails } from 'src/app/_interfaces/IEmployee';
import { RouterLink } from '@angular/router';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-employees-details-header',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    RouterLink,
    MatDialogModule,
  ],
  templateUrl: './employees-details-header.component.html',
  styleUrls: ['./employees-details-header.component.css'],
})
export class EmployeesDetailsHeaderComponent implements OnDestroy {
  @Input('employee') employee: IEmployeeDetails | undefined;
  @Output('delete') delete = new EventEmitter<number>();

  isProfileLoading: boolean = true;

  dialogRefSub: Subscription | undefined;

  constructor(private dialog: MatDialog) {}

  onDelete() {
    if (!this.employee) return;
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to delete ${this.employee.username}?`,
    };
    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    this.dialogRefSub = dialogRef.afterClosed().subscribe({
      next: (answer) => {
        if (!answer || !this.employee) return;
        this.delete.emit(this.employee.id);
      },
    });
  }

  ngOnDestroy(): void {
    this.dialogRefSub?.unsubscribe();
  }
}
