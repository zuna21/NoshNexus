import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatRippleModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MessageComponent } from './message/message.component';
import { ChatService } from 'src/app/_services/chat.service';
import { Subscription, mergeMap, of } from 'rxjs';
import { IChat, IMessage } from 'src/app/_interfaces/IChat';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ChatHubService } from 'src/app/_services/chat-hub.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatRippleModule,
    MatButtonModule,
    MessageComponent,
    ReactiveFormsModule
  ],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit, OnDestroy {
  @ViewChild('scrollContainer') scrollContainer: ElementRef | undefined;
  isOpen: boolean = true;
  chat: IChat | null = null;
  chatForm: FormGroup = this.fb.group({
    content: ['', Validators.required]
  });

  chatSub: Subscription | undefined;
  sendMessageSub: Subscription | undefined;
  receiveNewMessageSub: Subscription | undefined;
  receiveMyMessageSub: Subscription | undefined;

  constructor(
    private chatService: ChatService,
    private router: Router,
    private fb: FormBuilder,
    private chatHubService: ChatHubService
  ) { }

  ngOnInit(): void {
    this.getChat();
    this.receiveNewMessage();
    this.receiveMyMessage();
  }

  getChat() {
    this.chatSub = this.chatService.$chatId
      .pipe(
        mergeMap((chatId) => {
          if (!chatId) {
            return of(null);
          }
          return this.chatService.getChat(chatId);
        })
      )
      .subscribe({
        next: (chat) => {
          this.chat = chat;
          this.scrollToBottom();
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

  onSend() {
    if (this.chatForm.invalid || !this.chat) return;
    this.chatHubService.sendMessage(this.chat.id, this.chatForm.value);
  }

  openChat() {
    this.isOpen = !this.isOpen;
    if (this.isOpen) this.scrollToBottom();
  }

  afterMessageSend(newMessage: IMessage) {
    if (!newMessage || !this.chat) return;
    this.chat.messages.push(newMessage);
    this.scrollToBottom();
    this.chatForm.reset();
  }

  scrollToBottom() {
    setTimeout(() => {
      if (!this.scrollContainer) return;
      try {
        this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
      } catch(err) {}
    }, 0);
  }

  receiveNewMessage() {
    this.receiveNewMessageSub = this.chatHubService.newMessage$.subscribe({
      next: newMessage => {
        this.afterMessageSend(newMessage);
      }
    });
  }

  receiveMyMessage() {
    this.receiveMyMessageSub = this.chatHubService.newMyMessage$.subscribe({
      next: newMessage => {
        this.afterMessageSend(newMessage);
      }
    });
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
    this.sendMessageSub?.unsubscribe();
    this.receiveNewMessageSub?.unsubscribe();
    this.receiveMyMessageSub?.unsubscribe();
  }
}
