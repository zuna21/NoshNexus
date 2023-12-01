import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { TimeAgoPipe } from 'src/app/_pipes/time-ago.pipe';
import { IMessage } from 'src/app/_interfaces/IChat';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatIconModule,
    TimeAgoPipe
  ],
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css'],
})
export class MessageComponent {
  @Input('message') message: IMessage | undefined;

  isProfileImageLoading: boolean = true;


}
