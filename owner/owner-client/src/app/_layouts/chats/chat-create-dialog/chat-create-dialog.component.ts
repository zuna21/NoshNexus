import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IChat } from 'src/app/_interfaces/IChat';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-chat-create-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    SearchBarComponent,
    MatChipsModule,
    MatIconModule,
    MatDividerModule
  ],
  templateUrl: './chat-create-dialog.component.html',
  styleUrls: ['./chat-create-dialog.component.css']
})
export class ChatCreateDialogComponent {
  selectedChat: IChat | null = null;

  constructor(
    public dialogRef: MatDialogRef<ChatCreateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IChat | null,
  ) {
    this.selectedChat = data;
  }
}
