import { Component } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatRippleModule,
    RouterLink
  ],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {

  constructor(
    private _location: Location
  ) {}

  onBack() {
    this._location.back();
  }

}
