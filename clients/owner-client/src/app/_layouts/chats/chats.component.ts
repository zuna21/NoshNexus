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
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { ChatHubService } from 'src/app/_services/chat-hub.service';

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
  editChatSub: Subscription | undefined;
  deleteChatSub: Subscription | undefined;
  onSearchChatSub: Subscription | undefined;
  liveChatPreviewSub: Subscription | undefined;
  receiveMyMessageSub: Subscription | undefined;
  receiveNewMessageSub: Subscription | undefined;
  myLiveChatPreviewSub: Subscription | undefined;

  constructor(
    private dialog: MatDialog,
    private chatService: ChatService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private chatHubService: ChatHubService
  ) { }

  ngOnInit(): void {
    this.getChats();
    this.getSelectedChat();
    this.getLiveChatPreview();
    this.getMyLiveChatPreview();
    this.receiveMyMessage();
    this.receiveNewMessage();
  }

  getChats() {
    this.chatSub = this.chatService.getChats().subscribe({
      next: (chats) => (this.chats = chats),
    });
  }

  onOpenNewChatDialog() {
    const dialogRef = this.dialog.open(OpenNewChatDialogComponent);
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

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);

    this.deleteChatSub = dialogRef.afterClosed().pipe(
      mergeMap(answer => {
        if (!answer || !this.selectedChat) return of(null);
        return this.chatService.deleteChat(this.selectedChat.id);
      })
    ).subscribe({
      next: deletedChatId => {
        if (!deletedChatId) return;
        this.chats = this.chats.filter(x => x.id !== deletedChatId);
        this.selectedChat = null;
      }
    })
  }

  onSelectChat(chatId: number) {
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
  

  onChatCreateDialog() {
    const dialogRef = this.dialog.open(ChatCreateDialogComponent);
    this.createChatSub = dialogRef.afterClosed().subscribe({
      next: (chat: IChat | null) => {
        if (!chat) return;
        this.onSelectChat(chat.id);
        this.chatHubService.joinGroup(chat.id);
      }
    });
  }

  onChatEditDialog() {
    if (!this.selectedChat) return;
    const dialogConfig: MatDialogConfig = {
      data: this.selectedChat
    };
    const dialogRef = this.dialog.open(ChatCreateDialogComponent, dialogConfig);
    this.editChatSub = dialogRef.afterClosed().subscribe({
      next: editedChat => {
        if (!editedChat || !this.selectedChat) return;
        this.selectedChat = {
          ...this.selectedChat,
          name: editedChat.name, 
          participants: editedChat.participants
        }
        this.updateChatPreview(this.selectedChat.messages[this.selectedChat.messages.length - 1]);
        this.chats.map(x => {
          if (x.id === this.selectedChat?.id) x.name = this.selectedChat.name;
        });
      }
    });
  }

  sendMessage() {
    if (this.chatForm.invalid || !this.selectedChat) return;
      this.chatHubService.sendMessage(this.selectedChat.id, this.chatForm.value);
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


  onSearchChat(sq: string) {
    this.onSearchChatSub = this.chatService.getChats(sq).subscribe({
      next: chats => {
        if (!chats) return;
        this.chats = [...chats];
      }
    });
  }

  getLiveChatPreview() {
    this.liveChatPreviewSub = this.chatHubService.newChatPreview$.subscribe({
      next: chatPreview => {
        const chatIndex = this.chats.findIndex(x => x.id === chatPreview.id);
        if (chatIndex < 0) {
          this.chats = [chatPreview, ...this.chats];
        } else {
          this.chats[chatIndex] = {...chatPreview};
        }
      }
    });
  }

  getMyLiveChatPreview() {
    this.myLiveChatPreviewSub = this.chatHubService.newMyChatPreview$.subscribe({
      next: chatPreview => {
        const chatIndex = this.chats.findIndex(x => x.id === chatPreview.id);
        if (chatIndex < 0) {
          this.chats = [chatPreview, ...this.chats];
        } else {
          this.chats[chatIndex] = {...chatPreview};
        }
      }
    })
  }

  receiveMyMessage() {
    this.receiveMyMessageSub =  this.chatHubService.newMyMessage$.subscribe({
      next: myMessage => {
        this.afterMessageSend(myMessage);
      }
    });
  }

  receiveNewMessage() {
    this.receiveNewMessageSub = this.chatHubService.newMessage$.subscribe({
      next: message => {
        this.afterMessageSend(message);
      }
    })
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
    this.dialogRefNewChatSub?.unsubscribe();
    this.chatQueryParamSub?.unsubscribe();
    this.createChatSub?.unsubscribe();
    this.sendMessageSub?.unsubscribe();
    this.editChatSub?.unsubscribe();
    this.deleteChatSub?.unsubscribe();
    this.onSearchChatSub?.unsubscribe();
    this.liveChatPreviewSub?.unsubscribe();
    this.receiveMyMessageSub?.unsubscribe();
    this.receiveNewMessageSub?.unsubscribe();
    this.myLiveChatPreviewSub?.unsubscribe();
  }
}
