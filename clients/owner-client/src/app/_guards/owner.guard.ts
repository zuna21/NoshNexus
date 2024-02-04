import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { jwtDecode } from 'jwt-decode';

export const ownerGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const token = accountService.getToken();
  if (!token) return false;
  const role = JSON.parse(JSON.stringify(jwtDecode(token))).role;
  if (role === "owner") return true;
  return false;
};