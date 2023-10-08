import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImageWithDeleteComponent } from 'src/app/_components/image-with-delete/image-with-delete.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ImageWithDeleteComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

}
