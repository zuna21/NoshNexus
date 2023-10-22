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
import { Subscription, mergeMap, of } from 'rxjs';
import { ChatService } from 'src/app/_services/chat.service';
import { MessageComponent } from 'src/app/_components/chat/message/message.component';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ChatCreateDialogComponent } from './chat-create-dialog/chat-create-dialog.component';

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
  selectedChat: IChat | null = null;

  chatSub: Subscription | undefined;
  dialogRefNewChatSub: Subscription | undefined;
  chatQueryParamSub: Subscription | undefined;

  constructor(
    private dialog: MatDialog,
    private chatService: ChatService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getChats();
    this.getSelectedChat();
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
    if (!this.selectedChat) return;
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want delete ${this.selectedChat.name}?`,
    };
    this.dialog.open(ConfirmationDialogComponent, dialogConfig);
  }

  onSelectChat(chatId: string) {
    const queryParams: Params = { chat: chatId };
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams,
        queryParamsHandling: 'merge',
      });
  }

  getSelectedChat() {
    this.chatQueryParamSub = this.activatedRoute.queryParams.pipe(
      mergeMap((params: Params) => {
        if (!params['chat']) return of(null);
        return this.chatService.getOwnerChat(params['chat']);
      })
    ).subscribe({
      next: chat => {
        this.selectedChat = chat; // Jer ce vratiti IChat | null;
      }
    });
  }

  onChatCreateDialog(isEdit: boolean) {
    const dialogConfig: MatDialogConfig = {
      data: isEdit ? this.selectedChat : null
    };

    this.dialog.open(ChatCreateDialogComponent, dialogConfig);
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
    this.dialogRefNewChatSub?.unsubscribe();
    this.chatQueryParamSub?.unsubscribe();
  }
}
