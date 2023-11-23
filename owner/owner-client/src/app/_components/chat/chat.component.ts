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
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

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
  isOpen: boolean = true;
  chat: IChat | null = null;
  chatForm: FormGroup = this.fb.group({
    content: ['', Validators.required]
  });

  chatSub: Subscription | undefined;
  sendMessageSub: Subscription | undefined;

  constructor(
    private chatService: ChatService,
    private router: Router,
    private fb: FormBuilder
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
          return this.chatService.getChat(chatId);
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

  onSend() {
    if (this.chatForm.invalid || !this.chat) return;
    this.sendMessageSub = this.chatService.createMessage(`${this.chat.id}`, this.chatForm.value)
      .subscribe({
        next: newMessage => {
          if (!newMessage || !this.chat) return;
          this.chat.messages = [...this.chat.messages, newMessage];
          this.chatForm.reset();
        } 
      });
  }

  ngOnDestroy(): void {
    this.chatSub?.unsubscribe();
    this.sendMessageSub?.unsubscribe();
  }
}
