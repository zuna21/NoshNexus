import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryModule, ImageItem } from 'ng-gallery';
import { LightboxModule } from 'ng-gallery/lightbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/_components/confirmation-dialog/confirmation-dialog.component';
import { Subscription } from 'rxjs';
import { MatChipsModule } from '@angular/material/chips';


@Component({
  selector: 'app-restaurants-details',
  standalone: true,
  imports: [CommonModule, GalleryModule, LightboxModule, MatDividerModule, MatButtonModule, MatIconModule, MatTooltipModule, MatDialogModule, MatChipsModule],
  templateUrl: './restaurants-details.component.html',
  styleUrls: ['./restaurants-details.component.css'],
})
export class RestaurantsDetailsComponent implements OnDestroy {

  dialogRefSub: Subscription | undefined;

  restaurantImages = [
    new ImageItem({
      src: 'assets/img/restaurants/restauran1.jpg',
      thumb: 'assets/img/restaurants/restauran1.jpg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran2.jpeg',
      thumb: 'assets/img/restaurants/restauran2.jpeg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran3.jpg',
      thumb: 'assets/img/restaurants/restauran3.jpg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran4.jpeg',
      thumb: 'assets/img/restaurants/restauran4.jpeg',
    }),
    new ImageItem({
      src: 'assets/img/restaurants/restauran5.jpg',
      thumb: 'assets/img/restaurants/restauran5.jpg',
    }),
  ];

  constructor(private dialog: MatDialog) { }

  onDelete() {
    const question = `Are you sure you want to delete restaurant {{ime restorana}}?`
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: question
    });

    this.dialogRefSub = dialogRef.afterClosed().subscribe({
      next: answer => {
        if (!answer) return;
        // Izbrisi restoran
        console.log('Delete restaurant');
      }
    });
  }


  ngOnDestroy(): void {
    this.dialogRefSub?.unsubscribe();
  }
}
