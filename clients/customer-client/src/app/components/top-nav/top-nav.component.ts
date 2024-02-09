import { Component, EventEmitter, Output } from '@angular/core';

import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatIconModule} from '@angular/material/icon'; 
import {MatButtonModule} from '@angular/material/button'; 

@Component({
  selector: 'app-top-nav',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './top-nav.component.html',
  styleUrl: './top-nav.component.css'
})
export class TopNavComponent {
  @Output('sideNavEmitter') sideNavEmitter = new EventEmitter<void>();

  onSidenav() {
    this.sideNavEmitter.emit();
  }
}
