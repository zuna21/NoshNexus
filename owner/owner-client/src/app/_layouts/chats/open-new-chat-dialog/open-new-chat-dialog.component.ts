import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { SideNavChatComponent } from '../side-nav-chat/side-nav-chat.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IChatPreview } from 'src/app/_interfaces/IChat';

@Component({
  selector: 'app-open-new-chat-dialog',
  standalone: true,
  imports: [
    CommonModule,
    SearchBarComponent,
    SideNavChatComponent,
    MatDividerModule,
    SideNavChatComponent,
  ],
  templateUrl: './open-new-chat-dialog.component.html',
  styleUrls: ['./open-new-chat-dialog.component.css'],
})
export class OpenNewChatDialogComponent {
  chats: IChatPreview[] = [];

  constructor(
    public dialogRef: MatDialogRef<OpenNewChatDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { chats: IChatPreview[] }
  ) {
    this.chats = data.chats;
  }

  onSelectedChat(chatId: string) {
    this.dialogRef.close(chatId);
  }
}
