import { Component, Input } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatChipsModule} from '@angular/material/chips'; 
import { IImageCard } from 'src/app/_interfaces/IImage';
import { FileSizePipe } from 'src/app/_pipes/file-size.pipe';

@Component({
  selector: 'app-image-with-delete',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage, MatButtonModule, MatIconModule, MatChipsModule, FileSizePipe],
  templateUrl: './image-with-delete.component.html',
  styleUrls: ['./image-with-delete.component.css']
})
export class ImageWithDeleteComponent {
  @Input('isMain') isMain: boolean = false;
  @Input('image') image: IImageCard | undefined;
}
