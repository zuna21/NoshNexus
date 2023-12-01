import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';

export const anonGuard: CanActivateFn = (route, state) => {
  const accountService: AccountService = inject(AccountService);
  const router: Router = inject(Router);

  if (accountService.isLoggedIn()) {
    router.navigateByUrl('/home');
    return false;
  }
  return true;
};
