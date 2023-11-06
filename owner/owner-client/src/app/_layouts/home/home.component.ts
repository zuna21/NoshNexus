import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderCardComponent } from 'src/app/_components/order-card/order-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    OrderCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {


}
