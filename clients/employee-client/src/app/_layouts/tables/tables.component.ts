import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ITableCard } from 'src/app/_interfaces/ITable';
import { TableService } from 'src/app/_services/table.service';
import { Subscription, mergeMap, of } from 'rxjs';
import { TableCardComponent } from 'src/app/_components/table-card/table-card.component';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-tables',
  standalone: true,
  imports: [
    CommonModule, 
    TableCardComponent, 
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.css'],
})
export class TablesComponent implements OnInit, OnDestroy {
  tables: ITableCard[] = [];

  tableSub: Subscription | undefined;
  dialogRefSub: Subscription | undefined;

  constructor(
    private tableService: TableService, 
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.getTables();
  }

  getTables() {
    this.tableSub = this.tableService.getOwnerTables().subscribe({
      next: (tables) => (this.tables = tables),
    });
  }

  onRemoveTable(tableId: number) {
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to remove this table?`,
    };
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    this.dialogRefSub = dialogRef.afterClosed().pipe(
      mergeMap(result => {
        if (!result) return of(null);
        return this.tableService.delete(tableId);
      })
    ).subscribe({
      next: isDeleted => {
        if (!isDeleted) return;
        this.tables = this.tables.filter(x => x.id !== tableId);
        this.snackBar.open("Successfully deleted table", "Ok", {duration: 2000, panelClass: 'success-snackbar'})
      }
    });
  }

  ngOnDestroy(): void {
    this.tableSub?.unsubscribe();
    this.dialogRefSub?.unsubscribe();
  }
}
