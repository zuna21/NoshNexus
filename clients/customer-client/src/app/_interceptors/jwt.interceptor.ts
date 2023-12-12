import { inject } from '@angular/core';
import {
  HttpInterceptorFn
} from '@angular/common/http';
import { AccountService } from '../_services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService: AccountService = inject(AccountService);
  const token = accountService.getToken();
  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  return next(req)
}