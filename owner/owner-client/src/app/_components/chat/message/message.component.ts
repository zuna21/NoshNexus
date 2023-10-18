import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { IMessage } from 'src/app/_interfaces/IMessage';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatIconModule,
  ],
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css'],
})
export class MessageComponent {
  @Input('message') message: IMessage | undefined;
  @Input('isMine') isMine: boolean = false;
  @Input('inMenuBtn') inMenuBtn: boolean = false;

  isProfileImageLoading: boolean = true;
}
