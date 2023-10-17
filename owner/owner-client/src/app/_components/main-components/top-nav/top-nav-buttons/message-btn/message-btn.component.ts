import { Component, ElementRef, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatRippleModule } from '@angular/material/core';
import { MessageMenuComponent } from './message-menu/message-menu.component';

@Component({
  selector: 'app-message-btn',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatBadgeModule,
    MatChipsModule,
    MatDividerModule,
    MatRippleModule,
    MessageMenuComponent
  ],
  templateUrl: './message-btn.component.html',
  styleUrls: ['./message-btn.component.css']
})
export class MessageBtnComponent {
  openChat: boolean = false;

  constructor(
    private eRef: ElementRef
  ) {}

  @HostListener('document:click', ['$event'])
  clickout(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.openChat = false;
    }
  }

  onAllAsRead() {
    
  }
}
