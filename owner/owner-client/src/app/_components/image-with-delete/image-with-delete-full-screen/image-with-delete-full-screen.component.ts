import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-image-with-delete-full-screen',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './image-with-delete-full-screen.component.html',
  styleUrls: ['./image-with-delete-full-screen.component.css']
})
export class ImageWithDeleteFullScreenComponent {
  imgUrl: string = '';
  isLoading: boolean = true;

  constructor(
    public dialogRef: MatDialogRef<ImageWithDeleteFullScreenComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.imgUrl = data;
  }

  onClose() {
    this.dialogRef.close();
  }
}
