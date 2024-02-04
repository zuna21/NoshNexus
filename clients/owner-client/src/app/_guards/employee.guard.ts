import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';

export const employeeGuard: CanActivateFn = (route, state) => {
  const accountService: AccountService = inject(AccountService);
  const role = accountService.getRole();
  if (role === "employee") return true;
  return false;
};
