import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FolderWindowComponent } from 'src/app/_components/folder-window/folder-window.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    FolderWindowComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
}
