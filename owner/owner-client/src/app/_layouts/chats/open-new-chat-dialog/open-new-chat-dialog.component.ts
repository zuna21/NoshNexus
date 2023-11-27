import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { SideNavChatComponent } from '../side-nav-chat/side-nav-chat.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialogRef } from '@angular/material/dialog';
import { IChatPreview } from 'src/app/_interfaces/IChat';
import { Subscription } from 'rxjs';
import { ChatService } from 'src/app/_services/chat.service';

@Component({
  selector: 'app-open-new-chat-dialog',
  standalone: true,
  imports: [
    CommonModule,
    SearchBarComponent,
    SideNavChatComponent,
    MatDividerModule,
    SideNavChatComponent,
  ],
  templateUrl: './open-new-chat-dialog.component.html',
  styleUrls: ['./open-new-chat-dialog.component.css'],
})
export class OpenNewChatDialogComponent implements OnInit, OnDestroy {
  chats: IChatPreview[] = [];

  chatSub: Subscription | undefined;
  searchChatSub: Subscription | undefined;

  constructor(
    public dialogRef: MatDialogRef<OpenNewChatDialogComponent>,
    private chatService: ChatService
  ) {}

  ngOnInit(): void {
    this.getChats();
  }


  getChats() {
    this.chatSub = this.chatService.getChats().subscribe({
      next: chats => {
        if (!chats) return;
        this.chats = [...chats]
      }
    });
  }

  onSearchChat(sqName: string) {
    this.searchChatSub = this.chatService.getChats(sqName).subscribe({
      next: chats => {
        if (!chats) return;
        this.chats = [...chats];
      }
    });
  }

  onSelectedChat(chatId: number) {
    this.dialogRef.close(chatId);
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
    this.searchChatSub?.unsubscribe();
  }
}
