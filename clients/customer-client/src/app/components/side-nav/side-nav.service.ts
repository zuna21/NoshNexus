import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SideNavService {
  $toggleSidenav = new Subject<void>();

  constructor() { }

  toggleSidenav() {
    this.$toggleSidenav.next();
  }
}
