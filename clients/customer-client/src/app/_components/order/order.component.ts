import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatBadgeModule} from '@angular/material/badge'; 
import { DragDropModule } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatBadgeModule,
    DragDropModule,
  ],
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {

  mousePosition = {
    x: 0,
    y: 0
  };
  
  onMouseDown(event: MouseEvent) {
    this.mousePosition.x = event.screenX;
    this.mousePosition.y = event.screenY;
  }
  
  onClick(event: MouseEvent) {
    if (
      this.mousePosition.x === event.screenX &&
      this.mousePosition.y === event.screenY
    ) {
      this.openDialog();
    }
  }

  openDialog() {
    console.log('Otvori dialog');
  }

}
