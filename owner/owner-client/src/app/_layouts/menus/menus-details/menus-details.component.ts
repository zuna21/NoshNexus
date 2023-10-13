import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-menus-details',
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  templateUrl: './menus-details.component.html',
  styleUrls: ['./menus-details.component.css']
})
export class MenusDetailsComponent {

}
