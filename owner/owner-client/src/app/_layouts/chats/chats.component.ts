import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
import { IChat, IChatPreview, IMessage } from 'src/app/_interfaces/IChat';
import { Subscription, mergeMap, of } from 'rxjs';
import { ChatService } from 'src/app/_services/chat.service';
import { MessageComponent } from 'src/app/_components/chat/message/message.component';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ChatCreateDialogComponent } from './chat-create-dialog/chat-create-dialog.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

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
    ReactiveFormsModule
  ],
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css'],
})
export class ChatsComponent implements OnInit, OnDestroy {
  @ViewChild('scrollContainer') scrollContainer: ElementRef | undefined;
  chats: IChatPreview[] = [];
  selectedChat: IChat | null = null;
  chatForm: FormGroup = this.fb.group({
    content: ['', Validators.required]
  });

  chatSub: Subscription | undefined;
  dialogRefNewChatSub: Subscription | undefined;
  chatQueryParamSub: Subscription | undefined;
  createChatSub: Subscription | undefined;
  sendMessageSub: Subscription | undefined;

  constructor(
    private dialog: MatDialog,
    private chatService: ChatService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.getChats();
    this.getSelectedChat();
  }

  getChats() {
    this.chatSub = this.chatService.getChats().subscribe({
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
        return this.chatService.getChat(params['chat']);
      })
    ).subscribe({
      next: chat => {
        this.selectedChat = chat; // Jer ce vratiti IChat | null;
        this.scrollToBottom();
        this.chats.map(x => {
          if (!this.selectedChat) return;
          if (x.id === this.selectedChat.id) x.isSeen = true;
        });
      }
    });
  }
  

  onChatCreateDialog(isEdit: boolean) {
    const dialogConfig: MatDialogConfig = {
      data: isEdit ? this.selectedChat : null
    };

    const dialogRef = this.dialog.open(ChatCreateDialogComponent, dialogConfig);
    this.createChatSub = dialogRef.afterClosed().subscribe({
      next: (chat: IChat | null) => {
        if (!chat) return;
        this.onSelectChat(`${chat.id}`)
      }
    })
  }

  sendMessage() {
    if (this.chatForm.invalid || !this.selectedChat) return;
    this.sendMessageSub = this.chatService.createMessage(this.selectedChat.id, this.chatForm.value)
      .subscribe({
        next: newMessage => {
          this.afterMessageSend(newMessage);
        }
      });
  }

  afterMessageSend(newMessage: IMessage) {
    if (!newMessage || !this.selectedChat) return;
    this.selectedChat.messages.push(newMessage);
    this.scrollToBottom();
    this.chatForm.reset();
    this.updateChatPreview(newMessage);
  }


  updateChatPreview(newMessage: IMessage) {
    this.chats.map(x => {
      if (x.id === this.selectedChat?.id) {
        x.lastMessage = newMessage
      }
    });

    const updatedChat = this.chats.find(x => x.id === this.selectedChat?.id);
    this.chats = this.chats.filter(x => x.id !== this.selectedChat?.id);
    if (!updatedChat) return;
    this.chats.unshift(updatedChat);

  }


  sendOnEnter(event: KeyboardEvent) {
    if (event.key === 'Enter') this.sendMessage();
  }

  scrollToBottom() {
    setTimeout(() => {
      if (!this.scrollContainer) return;
      try {
        this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
      } catch(err) {}
    }, 0);
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
    this.dialogRefNewChatSub?.unsubscribe();
    this.chatQueryParamSub?.unsubscribe();
    this.createChatSub?.unsubscribe();
    this.sendMessageSub?.unsubscribe();
  }
}
