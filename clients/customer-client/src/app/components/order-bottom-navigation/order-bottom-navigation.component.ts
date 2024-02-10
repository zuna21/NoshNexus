import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatRippleModule} from '@angular/material/core';


@Component({
  selector: 'app-order-bottom-navigation',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
    MatRippleModule
  ],
  templateUrl: './order-bottom-navigation.component.html',
  styleUrl: './order-bottom-navigation.component.css'
})
export class OrderBottomNavigationComponent {

}
