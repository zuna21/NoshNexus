import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { FolderItemComponent } from './folder-item/folder-item.component';
import { MatMenuModule } from '@angular/material/menu';
import { FileItemComponent } from './file-item/file-item.component';
import { IFolderWindowFolder } from 'src/app/_interfaces/IFolder';
import { FOLDERS_IN_FOLDER, ROOT_FOLDERS } from 'src/app/fake_data/folders';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

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
export class FolderWindowComponent implements OnInit, OnDestroy {
  @Input('ownerId') ownerId: string = '';
  @Input('belongsToTable') belongsToTable: string = '';

  folders: IFolderWindowFolder[] = [];

  getFoldersSub: Subscription | undefined;

  constructor(
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getFolders();
  }

  getFolders() {
    this.getFoldersSub = this.activatedRoute.queryParams.subscribe({
      next: params => {
        if (!params['belongsToFolder']){
          // uzeti root foldere
          this.folders = [...ROOT_FOLDERS];
          return;
        }
        // uzeti onom folderu kojem pripadaju
        this.folders = [...FOLDERS_IN_FOLDER];
      }
    });
  }

  ngOnDestroy(): void {
    this.getFoldersSub?.unsubscribe();
  }

}
