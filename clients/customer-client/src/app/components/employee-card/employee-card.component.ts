import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IEmployeeCard } from '../../interfaces/employee.interface';

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
  @Input('employee') employee?: IEmployeeCard;
  @Output('onViewMoreEmitter') onViewMoreEmitter = new EventEmitter<number>();
  @Output('onRestaurantEmitter') onRestaurantEmitter = new EventEmitter<number>();

  isBackgroundImageLoading = signal<boolean>(true);

  onViewMore() {
    if(!this.employee) return;
    this.onViewMoreEmitter.emit(this.employee.id);
  }

  onRestaurant() {
    if (!this.employee) return;
    this.onRestaurantEmitter.emit(this.employee.restaurant.id);
  }
}
