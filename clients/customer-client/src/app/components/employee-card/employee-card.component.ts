import { Component, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-employee-card',
  standalone: true,
  imports: [
    MatProgressSpinnerModule,
    MatButtonModule
  ],
  templateUrl: './employee-card.component.html',
  styleUrl: './employee-card.component.css'
})
export class EmployeeCardComponent {
  isBackgroundImageLoading = signal<boolean>(true);
}
