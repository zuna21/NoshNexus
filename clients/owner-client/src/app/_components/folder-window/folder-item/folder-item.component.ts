import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';
import { IFolderWindowFolder } from 'src/app/_interfaces/IFolder';
import { ActivatedRoute, Params, Router } from '@angular/router';

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
  @Input('folder') folder: IFolderWindowFolder | undefined;

  constructor(
    private dialog: MatDialog,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  onOpenFolder() {
    if (!this.folder) return;
    const queryParams: Params = { belongsToFolder: `${this.folder.id}` };
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams,
        queryParamsHandling: 'merge',
      });
  }


  onDelete() {
    const dialogConfig: MatDialogConfig = {
      data: 'Are you sure you want to delete this directory?'
    }
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
  }
}
