import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatRippleModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MessageComponent } from './message/message.component';
import { MatDividerModule } from '@angular/material/divider';
import { ChatService } from 'src/app/_services/chat.service';
import { IChat } from 'src/app/_interfaces/IMessage';
import { Subscription, mergeMap, of } from 'rxjs';

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
export class ChatComponent implements OnInit, OnDestroy {
  isOpen: boolean = true;
  chat: IChat | undefined;
  chatSub: Subscription | undefined;

  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.getChat();
  }

  getChat() {
    this.chatSub = this.chatService.$chatId
      .pipe(
        mergeMap((chatId) => {
          if (!chatId) {
            this.chat = undefined;
            return of(null);
          }
          return this.chatService.getOwnerChat(chatId);
        })
      )
      .subscribe({
        next: (chat) => {
          if (!chat) {
            this.chat = undefined;
            return;
          }
          this.chat = chat;
        },
      });
  }

  onClose(event: Event) {
    event.stopPropagation();
    this.chatService.setChatId(null);
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
  }
}
