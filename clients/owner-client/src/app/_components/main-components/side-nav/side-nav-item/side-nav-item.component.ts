import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-side-nav-item',
  standalone: true,
  imports: [
    CommonModule, 
    RouterLink, 
    RouterLinkActive,
    TranslateModule
  ],
  templateUrl: './side-nav-item.component.html',
  styleUrls: ['./side-nav-item.component.css']
})
export class SideNavItemComponent {
  @Input('name') name: string = '';
  @Input('url') url: string = '';
}
