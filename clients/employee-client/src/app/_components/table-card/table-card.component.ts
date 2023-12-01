import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ITableCard } from 'src/app/_interfaces/ITable';

@Component({
  selector: 'app-table-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatButtonModule],
  templateUrl: './table-card.component.html',
  styleUrls: ['./table-card.component.css'],
})
export class TableCardComponent {
  @Input('table') table: ITableCard | undefined;
  @Output('remove') remove = new EventEmitter<number>();

  onRemove() {
    if (!this.table) return;
    this.remove.emit(this.table.id);
  }
}
