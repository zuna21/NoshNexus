import { Component, OnInit } from '@angular/core';
import { ITopNav, TopNavService } from '../../components/top-nav/top-nav.service';
import { RestaurantCardComponent } from '../../components/restaurant-card/restaurant-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RestaurantCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(
    private topNavService: TopNavService
  ) {}

  ngOnInit(): void {
    this.setTopNav();
  }

  setTopNav() {
    const topNav: ITopNav = {
      hasDrawerBtn: true,
      title: 'Restaurants'
    };
    this.topNavService.setTopNav(topNav);
  }
}
