import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IUserCard } from 'src/app/_interfaces/IUser';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-user-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule
  ],
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent {
  @Input('user') user: IUserCard = {
    id: -1,
    firstName: 'Nosh',
    lastName: 'Nexus',
    profileImage: '',
    username: 'noshNexus21'
  };
  @Output('unblock') unblock = new EventEmitter<number>();

  onUnblock() {
    this.unblock.emit(this.user.id);
  }

  
}
