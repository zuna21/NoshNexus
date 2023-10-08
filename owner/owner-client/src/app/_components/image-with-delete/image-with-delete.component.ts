import { Component } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-image-with-delete',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  templateUrl: './image-with-delete.component.html',
  styleUrls: ['./image-with-delete.component.css']
})
export class ImageWithDeleteComponent {

}
