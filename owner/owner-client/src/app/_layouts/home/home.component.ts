import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessageComponent } from 'src/app/_components/chat/message/message.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MessageComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
}
