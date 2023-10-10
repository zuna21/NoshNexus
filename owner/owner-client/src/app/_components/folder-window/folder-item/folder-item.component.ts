import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import {MatMenuModule} from '@angular/material/menu'; 
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-folder-item',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatMenuModule,
    MatDialogModule
  ],
  templateUrl: './folder-item.component.html',
  styleUrls: ['./folder-item.component.css'],
})
export class FolderItemComponent {

  constructor(
    private dialog: MatDialog
  ) {}

  onDelete() {
    const dialogConfig: MatDialogConfig = {
      data: 'Are you sure you want to delete this directory?'
    }
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
  }
}
