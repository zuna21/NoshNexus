import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { IProfileHeader } from 'src/app/_interfaces/IProfileHeader';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-account-header',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    TranslateModule
  ],
  templateUrl: './account-header.component.html',
  styleUrls: ['./account-header.component.css']
})
export class AccountHeaderComponent implements OnDestroy {
  @Input('canEdit') canEdit: boolean = true;
  @Input('canDelete') canDelete: boolean = true
  @Input('accountHeader') accountHeader: IProfileHeader = {
    username: "johnny",
    firstName: "John",
    lastName: "Doe",
    backgroundImage: "https://noshnexus.com/images/default/default.png",
    profileImage: "https://noshnexus.com/images/default/default-profile.png"
  }
  @Output('delete') delete = new EventEmitter<boolean>();
  @Output('edit') edit = new EventEmitter<boolean>();

  isProfileLoading: boolean = true;

  constructor(
    private dialog: MatDialog
  ) {}

  dialogRefSub: Subscription | undefined;
  onDelete() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = `Are you sure you want to delete your account?`;
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    this.dialogRefSub = dialogRef.afterClosed().subscribe({
      next: answer => {
        if (!answer) return;
        this.delete.emit(true);
      }
    });
  }

  onEdit() {
    this.edit.emit(true);
  }

  ngOnDestroy(): void {
    this.dialogRefSub?.unsubscribe();
  }
}
