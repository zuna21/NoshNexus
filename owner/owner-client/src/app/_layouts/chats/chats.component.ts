import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MatDividerModule } from '@angular/material/divider';
import { SideNavChatComponent } from './side-nav-chat/side-nav-chat.component';

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [
    CommonModule,
    SearchBarComponent,
    MatDividerModule,
    SideNavChatComponent,
  ],
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css'],
})
export class ChatsComponent {}
