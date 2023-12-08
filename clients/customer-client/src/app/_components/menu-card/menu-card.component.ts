import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IMenuCard } from 'src/app/_interfaces/IMenu';

@Component({
  selector: 'app-menu-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './menu-card.component.html',
  styleUrls: ['./menu-card.component.css'],
})
export class MenuCardComponent {
  @Input('menu') menu: IMenuCard | undefined;
  @Input('disable') disable: boolean = false;
  @Output('viewMoreEmitter') viewMoreEmitter = new EventEmitter<number>();

  onViewMore() {
    if(!this.menu) return;
    this.viewMoreEmitter.emit(this.menu.id);
  }
}
