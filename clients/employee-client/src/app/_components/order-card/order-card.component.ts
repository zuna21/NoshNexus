import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { TimeAgoPipe } from 'src/app/_pipes/time-ago.pipe';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-card',
  standalone: true,
  imports: [
    CommonModule, 
    MatIconModule, 
    MatButtonModule, 
    MatDividerModule, 
    TimeAgoPipe,
    MatTabsModule,
    RouterLink
  ],
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css'],
})
export class OrderCardComponent {
  @Input('order') order?: IOrderCard;

  @Output('accept') accept = new EventEmitter<IOrderCard>();
  @Output('decline') decline = new EventEmitter<IOrderCard>();
  @Output('block') block = new EventEmitter<IOrderCard>();

  
  onAccept() {
    if (!this.order) return;
    this.accept.emit(this.order);
  }

  onDecline() {
    if (!this.order) return;
    this.decline.emit(this.order);
  }

  onBlock() {
    if (!this.order) return;
    this.block.emit(this.order);
  }
}
