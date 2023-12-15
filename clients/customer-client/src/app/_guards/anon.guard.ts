import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';

export const anonGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router: Router = inject(Router)

  if (!accountService.isLoggedIn()) return true;
  router.navigateByUrl('/restaurants');
  return false;
};
