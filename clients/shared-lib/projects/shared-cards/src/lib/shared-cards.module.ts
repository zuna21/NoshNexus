import { NgModule } from '@angular/core';
import { SharedCardsComponent } from './shared-cards.component';
import { RestaurantCardComponent } from './restaurant-card/restaurant-card.component';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [
    SharedCardsComponent,
    RestaurantCardComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatTooltipModule
  ],
  exports: [
    SharedCardsComponent,
    RestaurantCardComponent
  ]
})
export class SharedCardsModule { }
