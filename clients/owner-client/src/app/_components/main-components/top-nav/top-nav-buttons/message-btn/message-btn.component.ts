import {
  Component,
  ElementRef,
  HostListener,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { Subscription } from 'rxjs';
import { ChatService } from 'src/app/_services/chat.service';
import { RouterLink } from '@angular/router';
import { IChatMenu, IChatPreview } from 'src/app/_interfaces/IChat';
import { SideNavChatComponent } from 'src/app/_layouts/chats/side-nav-chat/side-nav-chat.component';
import { ChatHubService } from 'src/app/_services/chat-hub.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-message-btn',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatBadgeModule,
    MatChipsModule,
    MatRippleModule,
    MatDividerModule,
    RouterLink,
    SideNavChatComponent,
    TranslateModule
  ],
  templateUrl: './message-btn.component.html',
  styleUrls: ['./message-btn.component.css'],
})
export class MessageBtnComponent implements OnInit, OnDestroy {
  openMessages: boolean = false;
  chatsMenu: IChatMenu | undefined;

  chatMenuSub: Subscription | undefined;
  markAsReadSub: Subscription | undefined;
  receiveChatPreviewSub?: Subscription;
  receiveMyChatPreviewSub?: Subscription;


  constructor(
    private eRef: ElementRef, 
    private chatService: ChatService,
    private chatHubService: ChatHubService
  ) { }

  ngOnInit(): void {
    this.getChats();
    this.receiveChatPreview();
    this.receiveMyChatPreview();
  }

  @HostListener('document:click', ['$event'])
  clickout(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.openMessages = false;
    }
  }

  getChats() {
    this.chatMenuSub = this.chatService.getChatsForMenu().subscribe({
      next: (chatMenu) => (this.chatsMenu = chatMenu),
    });
  }

  onSelectedChat(chatId: number) {
    this.chatService.setChatId(chatId);
    this.openMessages = false;
  }

  onAllAsRead() {
    this.markAsReadSub = this.chatService.markAllAsRead().subscribe({
      next: isAllRead => {
        if (!isAllRead || !this.chatsMenu) return;
        this.chatsMenu.chats.map(x => x.isSeen = true);
        this.chatsMenu.notSeenNumber = 0;
      }
    })
  }

  receiveChatPreview() {
    this.receiveChatPreviewSub = this.chatHubService.newChatPreview$.subscribe({
      next: chatPreview => {
        this.updateChatPreviews(chatPreview);
      }
    })
  }

  receiveMyChatPreview() {
    this.receiveChatPreviewSub = this.chatHubService.newMyChatPreview$.subscribe({
      next: chatPreview => {
        this.updateChatPreviews(chatPreview);
      }
    })
  }


  updateChatPreviews(chatPreview: IChatPreview) {
    if (!this.chatsMenu) return;
    const chatIndex = this.chatsMenu.chats.findIndex(x => x.id == chatPreview.id);
    if (chatIndex === -1) {
      this.chatsMenu.chats = [chatPreview, ...this.chatsMenu.chats];
    } else {
      this.chatsMenu.chats[chatIndex] = {...chatPreview};
    }

    this.chatsMenu.notSeenNumber = this.calculateNotSeenMessages();
  }

  calculateNotSeenMessages(): number {
    if (!this.chatsMenu) return 0;
    let notSeen = 0;
    for(let chat of this.chatsMenu.chats) {
      if (!chat.isSeen) notSeen++;
    }

    return notSeen;
  }


  ngOnDestroy(): void {
    this.chatMenuSub?.unsubscribe();
    this.receiveChatPreviewSub?.unsubscribe();
    this.receiveMyChatPreviewSub?.unsubscribe();
  }
}
