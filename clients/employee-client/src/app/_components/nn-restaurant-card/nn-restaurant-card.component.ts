import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';

interface IRestaurantCard {
    id: number;
    profileImage: string;
    name: string;
    isOpen: boolean;
    country: string;
    city: string;
    address: string;
}

@Component({
  selector: 'app-nn-restaurant-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule
  ],
  templateUrl: './nn-restaurant-card.component.html',
  styleUrls: ['./nn-restaurant-card.component.css']
})
export class NnRestaurantCardComponent {
  @Input('restaurant') restaurant: IRestaurantCard = {
    id: -1,
    profileImage: 'https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi0.wp.com%2Fwww.designlike.com%2Fwp-content%2Fuploads%2F2018%2F03%2Frestaurant-1948732_1920.jpg&f=1&nofb=1&ipt=848476e5ee42a9afd12a2d49f942b160ceadb4672f1a2844f2631fa4f1359d9d&ipo=images',
    name: 'Restaurnat Name',
    isOpen: true,
    country: 'Bosnia and Herzegovina',
    address: 'Restaurant Adress b.b.',
    city: 'Restaurant city'
  }
  @Output('seeMoreEmitter') seeMoreEmitter = new EventEmitter<number>();

  onSeeMore() {
    if (this.restaurant.id <= 0) return; 
    this.seeMoreEmitter.emit(this.restaurant.id);
  }

}
