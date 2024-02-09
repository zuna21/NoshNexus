import { NgStyle } from '@angular/common';
import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'; 

export interface IRestaurantCard {
  id: number;
  profileImage: string;
  name: string;
  isOpen: boolean;
  country: string;
  city: string;
  address: string;
}

@Component({
  selector: 'app-restaurant-card',
  standalone: true,
  imports: [
    MatProgressSpinnerModule,
    MatIconModule,
    NgStyle
  ],
  templateUrl: './restaurant-card.component.html',
  styleUrl: './restaurant-card.component.css'
})
export class RestaurantCardComponent {
  @Input('restaurant') restaurant: IRestaurantCard = {
    address: "Nosh Nexus b.b.",
    city: "Doboj",
    country: "Bosnia and Hercegovina",
    id: -1,
    isOpen: true,
    name: "Nosh Nexus",
    profileImage: "https://noshnexus.com/images/default/default.png"
  };
  @Output('selectedRestaurantEmitter') selectedRestaurantEmitter = new EventEmitter<number>();

  isImageLoading = signal<boolean>(true);

  onRestaurant() {
    this.selectedRestaurantEmitter.emit(this.restaurant.id);
  }

}
