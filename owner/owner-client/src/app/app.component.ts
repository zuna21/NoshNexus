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
    iconRegistry.addSvgIcon(
      'home',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/home.svg')
    );
    iconRegistry.addSvgIcon(
      'restaurant',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/restaurant.svg')
    );
    iconRegistry.addSvgIcon(
      'users',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/users.svg')
    );
    iconRegistry.addSvgIcon(
      'menu',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/menu.svg')
    );
    iconRegistry.addSvgIcon(
      'arrow-down',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/arrow-down.svg')
    );
    iconRegistry.addSvgIcon(
      'arrow-up',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/arrow-up.svg')
    );
    iconRegistry.addSvgIcon(
      'earth',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/earth.svg')
    );
    iconRegistry.addSvgIcon(
      'open',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/open.svg')
    );
    iconRegistry.addSvgIcon(
      'close',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/close.svg')
    );
    iconRegistry.addSvgIcon(
      'edit',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/edit.svg')
    );
    iconRegistry.addSvgIcon(
      'delete',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/delete.svg')
    );
    iconRegistry.addSvgIcon(
      'bullhorn',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/bullhorn.svg')
    );
    iconRegistry.addSvgIcon(
      'xmark',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/xmark.svg')
    );
    iconRegistry.addSvgIcon(
      'arrow-left',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/arrow-left.svg')
    );
    iconRegistry.addSvgIcon(
      'folder',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/folder.svg')
    );
    iconRegistry.addSvgIcon(
      'file',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/file.svg')
    );
    iconRegistry.addSvgIcon(
      'bars',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/bars.svg')
    );
    iconRegistry.addSvgIcon(
      'correct-circle',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/correct-circle.svg')
    );
    iconRegistry.addSvgIcon(
      'user',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/user.svg')
    );
    iconRegistry.addSvgIcon(
      'edit-user',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/edit-user.svg')
    );
    iconRegistry.addSvgIcon(
      'logout',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/logout.svg')
    );
    iconRegistry.addSvgIcon(
      'notification',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/notification.svg')
    );
    iconRegistry.addSvgIcon(
      'clock',
      sanitizer.bypassSecurityTrustResourceUrl('assets/svg/clock.svg')
    );
  }
}
