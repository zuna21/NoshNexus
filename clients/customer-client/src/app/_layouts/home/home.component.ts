import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemRowComponent } from 'src/app/_components/menu-item-row/menu-item-row.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MenuItemRowComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

}
