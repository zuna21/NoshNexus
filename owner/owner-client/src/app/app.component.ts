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
  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon(
      'facebook-logo',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/facebook-logo.svg')
    );
    iconRegistry.addSvgIcon(
      'instagram-logo',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/instagram-logo.svg')
    );
    iconRegistry.addSvgIcon(
      'globe',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/globe.svg')
    );
    iconRegistry.addSvgIcon(
      'image',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/image.svg')
    );
    iconRegistry.addSvgIcon(
      'images',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/images.svg')
    );
    iconRegistry.addSvgIcon(
      'save',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/save.svg')
    );
  }
}
