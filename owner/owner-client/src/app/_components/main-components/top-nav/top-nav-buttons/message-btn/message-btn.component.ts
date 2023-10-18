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
import { MatDividerModule } from '@angular/material/divider';
import { MatRippleModule } from '@angular/material/core';
import { MessageComponent } from 'src/app/_components/chat/message/message.component';
import { MessageService } from 'src/app/_services/message.service';
import { IMessagesForMenu } from 'src/app/_interfaces/IMessage';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-message-btn',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatBadgeModule,
    MatChipsModule,
    MatDividerModule,
    MatRippleModule,
    MessageComponent,
  ],
  templateUrl: './message-btn.component.html',
  styleUrls: ['./message-btn.component.css'],
})
export class MessageBtnComponent implements OnInit, OnDestroy {
  openMessages: boolean = false;
  messagesMenu: IMessagesForMenu | undefined;

  messageSub: Subscription | undefined;

  constructor(
    private eRef: ElementRef,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.getMessages();
  }

  @HostListener('document:click', ['$event'])
  clickout(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.openMessages = false;
    }
  }

  getMessages() {
    this.messageSub = this.messageService.getOwnerMessagesForMenu().subscribe({
      next: (messages) => (this.messagesMenu = messages),
    });
  }

  onAllAsRead() {}

  onClick() {
    console.log('Radi li ovo');
  }

  ngOnDestroy(): void {
    this.messageSub?.unsubscribe();
  }
}
