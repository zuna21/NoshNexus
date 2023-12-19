import { NgModule } from '@angular/core';
import { SharedCardsComponent } from './shared-cards.component';
import { NnRestaurantCardComponent } from '../public-api';



@NgModule({
  declarations: [
    SharedCardsComponent,
  ],
  imports: [
    NnRestaurantCardComponent
  ],
  exports: [
    SharedCardsComponent,
    NnRestaurantCardComponent
  ]
})
export class SharedCardsModule { }
