import { NgModule } from '@angular/core';
import { SharedCardsComponent } from './shared-cards.component';
import { RestaurantCardComponent } from './restaurant-card/restaurant-card.component';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CommonModule } from '@angular/common';
import { OrderCardComponent } from './order-card/order-card.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatTabsModule } from '@angular/material/tabs';
import { TimeAgoPipe } from './time-ago.pipe';



@NgModule({
  declarations: [
    SharedCardsComponent,
    RestaurantCardComponent,
    OrderCardComponent,
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatTooltipModule,
    MatDividerModule,
    MatTabsModule,
    TimeAgoPipe
  ],
  exports: [
    SharedCardsComponent,
    RestaurantCardComponent,
    OrderCardComponent
  ]
})
export class SharedCardsModule { }
