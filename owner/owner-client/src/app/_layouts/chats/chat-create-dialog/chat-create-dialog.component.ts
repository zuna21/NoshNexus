import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { IChat, IChatParticipant } from 'src/app/_interfaces/IChat';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';

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
    MatDividerModule,
    MatDialogModule,
    ReactiveFormsModule,
  ],
  templateUrl: './chat-create-dialog.component.html',
  styleUrls: ['./chat-create-dialog.component.css'],
})
export class ChatCreateDialogComponent implements OnInit, OnDestroy {
  selectedChat: IChat | null = null;
  chatForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
  });
  chatParticipants: IChatParticipant[] = [];

  confirmationDialogSub: Subscription | undefined;

  constructor(
    public dialogRef: MatDialogRef<ChatCreateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IChat | null,
    private fb: FormBuilder,
    private dialog: MatDialog
  ) {
    this.selectedChat = data;
    console.log(this.selectedChat);
  }

  ngOnInit(): void {
    this.initChatForm();
  }

  initChatForm() {
    if (!this.selectedChat) return;
    this.chatForm.patchValue({ name: this.selectedChat.name });
    this.chatParticipants = [...this.selectedChat.participants];
  }

  onRemoveChatParticipant(participant: IChatParticipant) {
    const dialogConfig: MatDialogConfig = {
      data: `Are you sure you want to remove ${participant.username}?`,
    };
    const confirmationDialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    this.confirmationDialogSub = confirmationDialogRef.afterClosed().subscribe({
      next: (answer) => {
        if (!answer) return;
        this.chatParticipants = this.chatParticipants.filter(
          (x) => x.id !== participant.id
        );
      },
    });
  }

  onClose() {
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
    this.confirmationDialogSub?.unsubscribe();
  }
}
