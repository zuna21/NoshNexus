import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ITableCard } from 'src/app/_interfaces/ITable';
import { TableService } from 'src/app/_services/table.service';
import { Subscription } from 'rxjs';
import { TableCardComponent } from 'src/app/_components/table-card/table-card.component';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-tables',
  standalone: true,
  imports: [CommonModule, TableCardComponent, MatDialogModule],
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.css'],
})
export class TablesComponent implements OnInit, OnDestroy {
  tables: ITableCard[] = [];

  tableSub: Subscription | undefined;

  constructor(private tableService: TableService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.getTables();
  }

  getTables() {
    this.tableSub = this.tableService.getOwnerTables().subscribe({
      next: (tables) => (this.tables = tables),
    });
  }

  onRemoveTable(tableId: string) {
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to remove this table?`,
    };
    this.dialog.open(ConfirmationDialogComponent, dialogConfig);
  }

  ngOnDestroy(): void {
    this.tableSub?.unsubscribe();
  }
}
