import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatRippleModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MessageComponent } from './message/message.component';
import { ChatService } from 'src/app/_services/chat.service';
import { Subscription, mergeMap, of } from 'rxjs';
import { IChat } from 'src/app/_interfaces/IChat';
import { Router } from '@angular/router';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatRippleModule,
    MatButtonModule,
    MessageComponent,
  ],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit, OnDestroy {
  isOpen: boolean = true;
  chat: IChat | null = null;
  chatSub: Subscription | undefined;

  constructor(
    private chatService: ChatService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getChat();
  }

  getChat() {
    this.chatSub = this.chatService.$chatId
      .pipe(
        mergeMap((chatId) => {
          if (!chatId) {
            return of(null);
          }
          return this.chatService.getOwnerChat(chatId);
        })
      )
      .subscribe({
        next: (chat) => {
          this.chat = chat;
        },
      });
  }

  onClose(event: Event) {
    event.stopPropagation();
    this.chatService.setChatId(null);
  }

  onClickChatName(event: Event) {
    event.stopPropagation();
    if (!this.chat) return;
    this.router.navigateByUrl(`chats?chat=${this.chat.id}`);
    this.chatService.setChatId(null);
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
  }
}
