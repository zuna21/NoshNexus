import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { IChatPreview } from 'src/app/_interfaces/IChat';
import { TimeAgoPipe } from 'src/app/_pipes/time-ago.pipe';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRippleModule } from '@angular/material/core';

@Component({
  selector: 'app-side-nav-chat',
  standalone: true,
  imports: [
    CommonModule, 
    MatIconModule, 
    TimeAgoPipe,
    MatProgressSpinnerModule,
    MatRippleModule
  ],
  templateUrl: './side-nav-chat.component.html',
  styleUrls: ['./side-nav-chat.component.css'],
})
export class SideNavChatComponent {
  @Input('chat') chat: IChatPreview | undefined;
  @Output('selectedChat') selectedChat = new EventEmitter<string>();

  isProfileImageLoading: boolean = true;

  onSelectedChat() {
    if (!this.chat) return;
    this.selectedChat.emit(this.chat.id);
  }
}
