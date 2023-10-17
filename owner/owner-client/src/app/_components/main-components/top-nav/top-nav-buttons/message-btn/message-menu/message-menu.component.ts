import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-message-menu',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule
  ],
  templateUrl: './message-menu.component.html',
  styleUrls: ['./message-menu.component.css']
})
export class MessageMenuComponent {

}
