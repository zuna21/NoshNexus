import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IMenuItemCard } from '../../interfaces/menu-item.interface';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DecimalPipe, NgStyle } from '@angular/common';

@Component({
  selector: 'app-menu-item-card',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
    NgStyle,
    DecimalPipe
  ],
  templateUrl: './menu-item-card.component.html',
  styleUrl: './menu-item-card.component.css'
})
export class MenuItemCardComponent {
  @Input('menuItem') menuItem?: IMenuItemCard;
  @Output('onAddEmitter') onAddEmitter = new EventEmitter<number>();

  onAdd(){
    if (!this.menuItem) return;
    this.onAddEmitter.emit(this.menuItem.id);
  }
}
