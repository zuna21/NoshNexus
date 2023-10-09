import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { IImageCard } from 'src/app/_interfaces/IImage';
import { FileSizePipe } from 'src/app/_pipes/file-size.pipe';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { ImageWithDeleteFullScreenComponent } from './image-with-delete-full-screen/image-with-delete-full-screen.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'; 
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-image-with-delete',
  standalone: true,
  imports: [
    CommonModule,
    NgOptimizedImage,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    FileSizePipe,
    MatDialogModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './image-with-delete.component.html',
  styleUrls: ['./image-with-delete.component.css'],
})
export class ImageWithDeleteComponent implements OnDestroy {
  @Input('isMain') isMain: boolean = false;
  @Input('image') image: IImageCard | undefined;
  @Output('deleteImage') deleteImage = new EventEmitter<string>();
  isLoading: boolean = true;
  dialogRefSub: Subscription | undefined;

  constructor(private dialog: MatDialog) {}

  onFullScreen() {
    if (!this.image) return;
    const dialogConfig: MatDialogConfig = {
      data: this.image.url,
      backdropClass: 'full-screen-image-backdrop'
    };
    this.dialog.open(ImageWithDeleteFullScreenComponent, dialogConfig);
  }

  onDelete(e: any) {
    e.stopPropagation();
    const dialogConfig: MatDialogConfig = {
      data: "Are you sure you want to delete this image?"
    };
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    this.dialogRefSub = dialogRef.afterClosed().subscribe({
      next: answer => {
        if (!answer || !this.image) return;
        this.deleteImage.emit(this.image.id)
      }
    })
  }

  ngOnDestroy(): void {
      this.dialogRefSub?.unsubscribe();
  }
}
