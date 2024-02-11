import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IMenuCard } from '../../interfaces/menu.interface';

@Component({
  selector: 'app-menu-card',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './menu-card.component.html',
  styleUrl: './menu-card.component.css'
})
export class MenuCardComponent {
  @Input('menu') menu?: IMenuCard;
  @Output('viewMoreEmitter') viewMoreEmitter = new EventEmitter<number>();

  onViewMore() {
    if (!this.menu) return;
    this.viewMoreEmitter.emit(this.menu.id);
  }
}
