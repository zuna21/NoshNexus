import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import {MatListModule} from '@angular/material/list';
import { FolderItemComponent } from './folder-item/folder-item.component';
import {MatMenuModule} from '@angular/material/menu';
import { FileItemComponent } from './file-item/file-item.component';

@Component({
  selector: 'app-folder-window',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatListModule,
    FolderItemComponent,
    MatMenuModule,
    FileItemComponent
  ],
  templateUrl: './folder-window.component.html',
  styleUrls: ['./folder-window.component.css'],
})
export class FolderWindowComponent {}
