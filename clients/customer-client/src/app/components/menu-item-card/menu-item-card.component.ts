import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IMenuItemCard } from '../../interfaces/menu-item.interface';
import { MatButtonModule } from '@angular/material/button';
import { DecimalPipe, NgStyle, TitleCasePipe } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-menu-item-card',
  standalone: true,
  imports: [
    MatButtonModule,
    NgStyle,
    DecimalPipe,
    TranslateModule,
    TitleCasePipe
  ],
  templateUrl: './menu-item-card.component.html',
  styleUrl: './menu-item-card.component.css'
})
export class MenuItemCardComponent {
  @Input('menuItem') menuItem?: IMenuItemCard;
  @Input('canRemove') canRemove: boolean = false;
  @Output('onAddEmitter') onAddEmitter = new EventEmitter<number>();
  @Output('onRemoveEmitter') onRemoveEmitter = new EventEmitter<number>();

  onAdd(){
    if (!this.menuItem) return;
    this.onAddEmitter.emit(this.menuItem.id);
  }

  onRemove() {
    if (!this.menuItem) return;
    this.onRemoveEmitter.emit(this.menuItem.id);
  }
}
