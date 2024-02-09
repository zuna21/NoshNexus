import { Injectable, signal } from '@angular/core';

export interface ITopNav {
  title: string;
  hasDrawerBtn: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TopNavService {
  topNav = signal<ITopNav>({
    hasDrawerBtn: true,
    title: "Nosh Nexus"
  })

  constructor() { }

  setTopNav(topNav: ITopNav) {
    this.topNav.set(topNav);
  }
}
