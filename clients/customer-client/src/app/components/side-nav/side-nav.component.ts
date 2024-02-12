import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import {MatDialog, MatDialogModule} from '@angular/material/dialog'; 
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    MatIconModule,
    RouterLink,
    MatDialogModule
  ],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.css'
})
export class SideNavComponent {

  constructor(
    private dialog: MatDialog
  ) {}


  onOpenLoginDialog() {
    this.dialog.open(LoginDialogComponent);
  }

}
