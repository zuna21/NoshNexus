import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRestaurantCard } from 'src/app/_interfaces/IRestaurant';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-restaurant-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatButtonModule,
    TranslateModule
  ],
  templateUrl: './restaurant-card.component.html',
  styleUrls: ['./restaurant-card.component.css']
})
export class RestaurantCardComponent {
  @Input('restaurant') restaurant: IRestaurantCard = {
    id: -1,
    name: 'Nosh Nexus',
    address: 'Nosh Nexus b.b.',
    city: 'Nexus',
    country: 'Bosnia and Herzegovina',
    isOpen: true,
    profileImage: 'https://noshnexus.com/images/default/default.png'
  }
  @Output('seeMore') seeMore = new EventEmitter<number>();

  isImageLoading: boolean = true;

  onSeeMore() {
    if (this.restaurant.id === -1) return;
    this.seeMore.emit(this.restaurant.id);
  }
}
