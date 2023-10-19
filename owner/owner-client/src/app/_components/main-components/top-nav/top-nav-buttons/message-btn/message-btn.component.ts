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
import { ChatMenuComponent } from './chat-menu/chat-menu.component';
import { MatDividerModule } from '@angular/material/divider';
import { IChatMenu } from 'src/app/_interfaces/IMessage';
import { Subscription } from 'rxjs';
import { ChatService } from 'src/app/_services/chat.service';
import { RouterLink } from '@angular/router';

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
    ChatMenuComponent,
    MatDividerModule,
    RouterLink
  ],
  templateUrl: './message-btn.component.html',
  styleUrls: ['./message-btn.component.css'],
})
export class MessageBtnComponent implements OnInit, OnDestroy {
  openMessages: boolean = false;
  chatsMenu: IChatMenu | undefined;

  chatMenuSub: Subscription | undefined;

  constructor(private eRef: ElementRef, private chatService: ChatService) {}

  ngOnInit(): void {
    this.getChats();
  }

  @HostListener('document:click', ['$event'])
  clickout(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.openMessages = false;
    }
  }

  getChats() {
    this.chatMenuSub = this.chatService.getOwnerChatForMenu().subscribe({
      next: (chatMenu) => (this.chatsMenu = chatMenu),
    });
  }

  onAllAsRead() {}

  onClick() {
    console.log('Radi li ovo');
  }

  ngOnDestroy(): void {
    this.chatMenuSub?.unsubscribe();
  }
}
