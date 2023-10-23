import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TableCardComponent } from 'src/app/_components/table-card/table-card.component';

@Component({
  selector: 'app-tables-create',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    TableCardComponent
  ],
  templateUrl: './tables-create.component.html',
  styleUrls: ['./tables-create.component.css']
})
export class TablesCreateComponent {

}
