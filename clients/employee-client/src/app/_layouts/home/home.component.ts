import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NnRestaurantCardComponent } from 'src/app/_components/nn-restaurant-card/nn-restaurant-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    NnRestaurantCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {


}
