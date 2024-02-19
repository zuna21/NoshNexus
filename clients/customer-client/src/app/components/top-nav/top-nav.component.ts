import { Component, EventEmitter, Output } from '@angular/core';

import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatIconModule} from '@angular/material/icon'; 
import {MatButtonModule} from '@angular/material/button'; 
import { TopNavService } from './top-nav.service';
import { AsyncPipe, Location } from '@angular/common';
import { SideNavService } from '../side-nav/side-nav.service';

@Component({
  selector: 'app-top-nav',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    AsyncPipe
  ],
  templateUrl: './top-nav.component.html',
  styleUrl: './top-nav.component.css'
})
export class TopNavComponent {
  @Output('sideNavEmitter') sideNavEmitter = new EventEmitter<void>();

  constructor(
    public topNavService: TopNavService,
    private location: Location,
    private sidenavService: SideNavService
  ) {}

  onSidenav() {
    this.sideNavEmitter.emit();
    this.sidenavService.toggleSidenav();
  }

  goBack() {
    this.location.back();
  }
}
