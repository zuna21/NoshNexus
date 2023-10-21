import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IChat, IChatParticipant } from 'src/app/_interfaces/IChat';

@Component({
  selector: 'app-add-new-participant-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    SearchBarComponent,
    MatChipsModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './add-new-participant-dialog.component.html',
  styleUrls: ['./add-new-participant-dialog.component.css']
})
export class AddNewParticipantDialogComponent {
  chat: IChat = this.data;

  constructor(
    public dialogRef: MatDialogRef<AddNewParticipantDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IChat,
  ) {
    console.log(this.chat);
  }

  remove(participant: IChatParticipant) {

  }
}
