import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {

  constructor(
    iconRegistry: MatIconRegistry, 
    sanitizer: DomSanitizer,
  ) {
    iconRegistry.addSvgIcon(
      'facebook-logo',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/facebook-logo.svg')
    );
    iconRegistry.addSvgIcon(
      'instagram-logo',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/instagram-logo.svg')
    );
    iconRegistry.addSvgIcon(
      'google-logo',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/google-logo.svg')
    );
    iconRegistry.addSvgIcon(
      'microsoft-logo',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/microsoft-logo.svg')
    );
  }

}
