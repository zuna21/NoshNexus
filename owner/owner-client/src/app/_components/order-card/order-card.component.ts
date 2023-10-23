import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { TimeAgoPipe } from 'src/app/_pipes/time-ago.pipe';

@Component({
  selector: 'app-order-card',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule, MatDividerModule, TimeAgoPipe],
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css'],
})
export class OrderCardComponent {
  @Input('order') order: IOrderCard | undefined;
}
