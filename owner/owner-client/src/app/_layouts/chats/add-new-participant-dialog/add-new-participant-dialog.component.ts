import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IChat } from 'src/app/_interfaces/IChat';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { SearchBarComponent } from 'src/app/_components/search-bar/search-bar.component';
import { MatDividerModule } from '@angular/material/divider';
import { ParticipantPreviewComponent } from './participant-preview/participant-preview.component';


@Component({
  selector: 'app-add-new-participant-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatChipsModule,
    MatIconModule,
    SearchBarComponent,
    MatDividerModule,
    ParticipantPreviewComponent
  ],
  templateUrl: './add-new-participant-dialog.component.html',
  styleUrls: ['./add-new-participant-dialog.component.css']
})
export class AddNewParticipantDialogComponent {
  chat: IChat = this.data;
  chatForm: FormGroup = this.fb.group({
    name: [this.chat.name, Validators.required]
  });

  constructor(
    public dialogRef: MatDialogRef<AddNewParticipantDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IChat,
    private fb: FormBuilder
  ) {
    console.log(this.chat);
  }

  remove(somehtin: any) {}

}
