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
import { IChatMenu } from 'src/app/_interfaces/IChat';
import { SideNavChatComponent } from 'src/app/_layouts/chats/side-nav-chat/side-nav-chat.component';
import { ChatHubService } from 'src/app/_services/chat-hub.service';
import { AccountService } from 'src/app/_services/account.service';

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
    SideNavChatComponent
  ],
  templateUrl: './message-btn.component.html',
  styleUrls: ['./message-btn.component.css'],
})
export class MessageBtnComponent implements OnInit, OnDestroy {
  openMessages: boolean = false;
  chatsMenu: IChatMenu | undefined;

  chatMenuSub: Subscription | undefined;
  userSub: Subscription | undefined;
  liveChatSub: Subscription | undefined;

  constructor(
    private eRef: ElementRef, 
    private chatService: ChatService,
    private chatHubService: ChatHubService,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.getChats();
    this.connectToLiveChats();
    this.getLiveChatPreview();
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

  markAsReadSub: Subscription | undefined;
  onAllAsRead() {
    this.markAsReadSub = this.chatService.markAllAsRead().subscribe({
      next: isAllRead => {
        if (!isAllRead || !this.chatsMenu) return;
        this.chatsMenu.chats.map(x => x.isSeen = true);
        this.chatsMenu.notSeenNumber = 0;
      }
    })
  }

  onClick() {
    console.log('Radi li ovo');
  }

  connectToLiveChats() {
    this.userSub = this.accountService.user$.subscribe({
      next: user => {
        if (!user) return;
        this.chatHubService.startConnection(user.username);
      }
    })
  }

  getLiveChatPreview() {
    this.liveChatSub = this.chatHubService.newChatPreview$.subscribe({
      next: chatPreview => {
        if (!this.chatsMenu) return;
        let chatIndex = this.chatsMenu.chats.findIndex(x => x.id === chatPreview.id);
        if (chatIndex < 0) {
          this.chatsMenu.chats = [chatPreview, ...this.chatsMenu.chats];
          this.chatsMenu.notSeenNumber++;
        } else {
          if (this.chatsMenu.chats[chatIndex].isSeen) this.chatsMenu.notSeenNumber++;
          this.chatsMenu.chats[chatIndex] = {...chatPreview};
        }
      }
    });
  }


  ngOnDestroy(): void {
    this.chatMenuSub?.unsubscribe();
    this.userSub?.unsubscribe();
    this.liveChatSub?.unsubscribe();
    this.chatHubService.stopConnection();
  }
}
