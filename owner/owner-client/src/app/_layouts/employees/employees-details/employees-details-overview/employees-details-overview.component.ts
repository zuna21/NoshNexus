import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatDividerModule} from '@angular/material/divider'; 
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-employees-details-overview',
  standalone: true,
  imports: [CommonModule, MatDividerModule, MatIconModule],
  templateUrl: './employees-details-overview.component.html',
  styleUrls: ['./employees-details-overview.component.css']
})
export class EmployeesDetailsOverviewComponent {

}
