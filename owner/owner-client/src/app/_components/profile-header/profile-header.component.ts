import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterLink } from '@angular/router';
import { IProfileHeader } from 'src/app/_interfaces/IProfileHeader';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-profile-header',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    RouterLink,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.css'],
})
export class ProfileHeaderComponent implements OnDestroy {
  @Input('profileHeader') profileHeader: IProfileHeader = {
    id: '',
    backgroundImage: 'assets/img/default.png',
    profileImage: 'assets/img/default-profile.png',
    firstName: 'name',
    lastName: 'surname',
    username: 'john_doe21'
  }
  @Input('edit') edit: boolean = true;
  @Input('delete') delete: boolean = true;
  @Output('onDelete') onDelete = new EventEmitter<string>();
  @Output('onEdit') onEdit = new EventEmitter<string>();

  isProfileLoading: boolean = true;

  dialogRefSub: Subscription | undefined;

  constructor(private dialog: MatDialog) {}

  onEditClick() {
    this.onEdit.emit(this.profileHeader.id);
  }

  onDeleteClick() {
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to delete ${this.profileHeader.username}?`,
    };
    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    this.dialogRefSub = dialogRef.afterClosed().subscribe({
      next: (answer) => {
        if (!answer) return;
        this.onDelete.emit(this.profileHeader.id);
      },
    });
  }

  ngOnDestroy(): void {
    this.dialogRefSub?.unsubscribe();
  }
}
