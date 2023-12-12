import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

export const authGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);
  const accountService = inject(AccountService);

  if (accountService.isLoggedIn()) return true;

  router.navigateByUrl('/login');
  return false;
};
