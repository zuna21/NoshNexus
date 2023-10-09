import { Component, Input } from '@angular/core';
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
export class ImageWithDeleteComponent {
  @Input('isMain') isMain: boolean = false;
  @Input('image') image: IImageCard | undefined;
  isLoading: boolean = true;

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
    console.log('on Delete');
  }
}
