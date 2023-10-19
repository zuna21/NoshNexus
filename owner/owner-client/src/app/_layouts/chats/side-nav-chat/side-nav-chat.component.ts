import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-side-nav-chat',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule
  ],
  templateUrl: './side-nav-chat.component.html',
  styleUrls: ['./side-nav-chat.component.css']
})
export class SideNavChatComponent {

}
