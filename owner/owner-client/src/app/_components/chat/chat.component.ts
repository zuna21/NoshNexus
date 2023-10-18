import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatRippleModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MessageComponent } from './message/message.component';
import { MatDividerModule } from '@angular/material/divider';
import { ChatService } from 'src/app/_services/chat.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { flatMap, mergeMap, of, switchMap } from 'rxjs';
import { IChat } from 'src/app/_interfaces/IMessage';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatRippleModule,
    MatButtonModule,
    MessageComponent,
    MatDividerModule,
  ],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit, OnDestroy {
  isOpen: boolean = false;
  chat: IChat | undefined;

  constructor(
    private chatService: ChatService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getChat();
  }

  getChat() {
    // uzimam chat iz query parmetara //url?chat=chatId
    this.activatedRoute.queryParams.pipe(
      switchMap(params => {
        const chatId = params['chat'];
        if (!chatId) return of(null);
        return this.chatService.getOwnerChat(chatId);
      })
    ).subscribe(chat => {
      this.chat = chat ? chat : undefined;
      return;
    });
  }

  onClose(event: Event) {
    event.stopPropagation();
    console.log("On Close")

    this.router.navigate([], {
      queryParams: {
        'chat': null
      },
      queryParamsHandling: 'merge'
    });
  }

  ngOnDestroy(): void {

  }
}
