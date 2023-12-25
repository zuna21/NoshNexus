import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IRestaurantCard } from './restaurant-card.interface';

@Component({
  selector: 'lib-restaurant-card',
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
    profileImage: 'http://localhost:5000/images/default/default.png'
  }
  @Output('seeMore') seeMore = new EventEmitter<number>();

  isImageLoading: boolean = true;

  onSeeMore() {
    if (this.restaurant.id === -1) return;
    this.seeMore.emit(this.restaurant.id);
  }
}
