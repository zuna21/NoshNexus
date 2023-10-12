import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuCardComponent } from 'src/app/_components/menu-card/menu-card.component';

@Component({
  selector: 'app-menus',
  standalone: true,
  imports: [CommonModule, MenuCardComponent],
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css']
})
export class MenusComponent {

}
