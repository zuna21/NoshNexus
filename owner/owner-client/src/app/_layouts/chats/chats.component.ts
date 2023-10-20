import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MatDividerModule } from '@angular/material/divider';
import { SideNavChatComponent } from './side-nav-chat/side-nav-chat.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
} from '@angular/material/dialog';
import { OpenNewChatDialogComponent } from './open-new-chat-dialog/open-new-chat-dialog.component';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { IChat, IChatPreview } from 'src/app/_interfaces/IChat';
import { Subscription } from 'rxjs';
import { ChatService } from 'src/app/_services/chat.service';
import { MessageComponent } from 'src/app/_components/chat/message/message.component';

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [
    CommonModule,
    SearchBarComponent,
    SideNavChatComponent,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    MatMenuModule,
    MatDialogModule,
    MessageComponent,
    MatDividerModule,
  ],
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css'],
})
export class ChatsComponent implements OnInit, OnDestroy {
  chats: IChatPreview[] = [];
  selectedChat: IChat | undefined;

  chatSub: Subscription | undefined;
  selectedChatSub: Subscription | undefined;
  dialogRefNewChatSub: Subscription | undefined;

  constructor(private dialog: MatDialog, private chatService: ChatService) {}

  ngOnInit(): void {
    this.getChats();
  }

  getChats() {
    this.chatSub = this.chatService.getOwnerChats().subscribe({
      next: (chats) => (this.chats = chats),
    });
  }

  onOpenNewChatDialog() {
    const dialogConfig: MatDialogConfig = {
      data: { chats: this.chats },
    };
    const dialogRef = this.dialog.open(
      OpenNewChatDialogComponent,
      dialogConfig
    );
    this.dialogRefNewChatSub = dialogRef.afterClosed().subscribe({
      next: (chatId) => {
        if (!chatId) return;
        this.onSelectChat(chatId);
      },
    });
  }

  onDeleteChat() {
    const dialogConfig: MatDialogConfig = {
      data: 'Are you sure you want to delete {{chatName}}?',
    };
    this.dialog.open(ConfirmationDialogComponent, dialogConfig);
  }

  onSelectChat(chatId: string) {
    if (!chatId) return;
    this.selectedChatSub = this.chatService.getOwnerChat(chatId).subscribe({
      next: (chat) => (this.selectedChat = chat),
    });
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
    this.selectedChatSub?.unsubscribe();
    this.dialogRefNewChatSub?.unsubscribe();
  }
}
