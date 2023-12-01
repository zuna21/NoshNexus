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
import { FOLDERS_IN_FOLDER, OPENED_FOLDER, ROOT_FOLDERS } from 'src/app/fake_data/folders';
import { ActivatedRoute, Params, Router } from '@angular/router';
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

  openedFolder: IFolderWindowFolder = {   // Ovaj ce uglavnom sluziti za navigaciju
    id: '',
    belongsTo: '',
    name: '',
    description: '',
    createdAt: ''
  }
  folders: IFolderWindowFolder[] = [];

  getFoldersSub: Subscription | undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getFolders();
  }

  getFolders() {
    this.getFoldersSub = this.activatedRoute.queryParams.subscribe({
      next: params => {
        if (!params['belongsToFolder']) {
          // uzeti root foldere
          this.folders = [...ROOT_FOLDERS];
          this.inRootFolder();
          return;
        }
        // uzeti onom folderu kojem pripadaju
        this.folders = [...FOLDERS_IN_FOLDER];
        this.openedFolder = OPENED_FOLDER;
      }
    });
  }

  inRootFolder() {
    this.openedFolder = {
      id: '',
      belongsTo: '',
      name: '',
      description: '',
      createdAt: ''
    }
  }

  goBack() {
    const queryParams: Params = { belongsToFolder: this.openedFolder.belongsTo === '' ? null : this.openedFolder.belongsTo };
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams,
        queryParamsHandling: 'merge',
      });
  }

  ngOnDestroy(): void {
    this.getFoldersSub?.unsubscribe();
  }

}
