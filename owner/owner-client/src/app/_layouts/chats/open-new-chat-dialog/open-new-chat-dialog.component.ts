import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { SideNavChatComponent } from '../side-nav-chat/side-nav-chat.component';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-open-new-chat-dialog',
  standalone: true,
  imports: [
    CommonModule,
    SearchBarComponent,
    SideNavChatComponent,
    MatDividerModule
  ],
  templateUrl: './open-new-chat-dialog.component.html',
  styleUrls: ['./open-new-chat-dialog.component.css']
})
export class OpenNewChatDialogComponent {

}
