import { BreakpointObserver } from '@angular/cdk/layout';
import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';

export const desktopGuard: CanDeactivateFn<unknown> = (component, currentRoute, currentState, nextState) => {
  const breakpointObserver: BreakpointObserver = inject(BreakpointObserver);

  const isSmallScreen = breakpointObserver.isMatched('(max-width: 599px)');
  
  if (isSmallScreen) return true;
  else return false;
};
