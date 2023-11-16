import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { IMenuItemCard } from 'src/app/_interfaces/IMenu';
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-menu-item-card',
  standalone: true,
  imports: [
    CommonModule, 
    MatCardModule, 
    MatButtonModule, 
    MatDialogModule, 
    MatProgressSpinnerModule,
    RouterLink
  ],
  templateUrl: './menu-item-card.component.html',
  styleUrls: ['./menu-item-card.component.css'],
})
export class MenuItemCardComponent implements OnDestroy {
  @Input('menuItem') menuItem: IMenuItemCard | undefined;
  @Output('delete') delete = new EventEmitter<number>();

  isImageLoading: boolean = true;

  dialogRefSub: Subscription | undefined;

  constructor(
    private dialog: MatDialog
  ) { }

  onDelete() {
    if (!this.menuItem) return;
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to delete ${this.menuItem.name}?`
    };
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    this.dialogRefSub = dialogRef.afterClosed().subscribe({
      next: answer => {
        if (!answer || !this.menuItem) return;
        this.delete.emit(this.menuItem.id);
      }
    })
  }

  ngOnDestroy(): void {
    this.dialogRefSub?.unsubscribe();
  }
}
