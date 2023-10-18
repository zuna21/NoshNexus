import {
  Component,
  ElementRef,
  HostListener,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';

@Component({
  selector: 'app-message-btn',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatBadgeModule,
    MatChipsModule,
    MatRippleModule,
  ],
  templateUrl: './message-btn.component.html',
  styleUrls: ['./message-btn.component.css'],
})
export class MessageBtnComponent {
  openMessages: boolean = false;

  constructor(
    private eRef: ElementRef,
  ) {}



  @HostListener('document:click', ['$event'])
  clickout(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.openMessages = false;
    }
  }



  onAllAsRead() {}

  onClick() {
    console.log('Radi li ovo');
  }


}
