import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatIconModule
  ],
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent {
  isProfileImageLoading: boolean = true;
}
