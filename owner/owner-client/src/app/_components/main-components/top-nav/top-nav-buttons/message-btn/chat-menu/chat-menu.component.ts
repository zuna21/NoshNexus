import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { IChatMenuSingle } from 'src/app/_interfaces/IMessage';
import { TimeAgoPipe } from 'src/app/_pipes/time-ago.pipe';
import { ChatService } from 'src/app/_services/chat.service';

@Component({
  selector: 'app-chat-menu',
  standalone: true,
  imports: [CommonModule, MatIconModule, TimeAgoPipe],
  templateUrl: './chat-menu.component.html',
  styleUrls: ['./chat-menu.component.css'],
})
export class ChatMenuComponent {
  @Input('chatMenu') chatMenu: IChatMenuSingle | undefined;

  constructor(private chatService: ChatService) {}

  onChatMenu() {
    if (!this.chatMenu) return;
    this.chatService.setChatId(this.chatMenu.id);
  }
}
