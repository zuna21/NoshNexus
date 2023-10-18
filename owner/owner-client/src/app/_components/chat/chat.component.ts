import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatRippleModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MessageComponent } from './message/message.component';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatRippleModule,
    MatButtonModule,
    MessageComponent,
    MatDividerModule,
  ],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent {
  isOpen: boolean = false;

  onClose(event: Event) {
    event.stopPropagation();
    console.log("On Close")
  }
}
