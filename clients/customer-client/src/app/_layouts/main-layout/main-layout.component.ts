import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from 'src/app/_components/header/header.component';
import {MatSidenavModule} from '@angular/material/sidenav'; 
import { OrderComponent } from 'src/app/_components/order/order.component';
import { OrderStore } from 'src/app/_stores/order.store';
import { SideNavComponent } from 'src/app/_components/side-nav/side-nav.component';


@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HeaderComponent,
    MatSidenavModule,
    OrderComponent,
    SideNavComponent
  ],
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent {

  constructor(public orderStore: OrderStore) {}
}
