import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService: AccountService = inject(AccountService);
  const router: Router = inject(Router);


  if (!accountService.isLoggedIn())  {
    router.navigateByUrl('/login');
    return false;
  }

  return true;
};
