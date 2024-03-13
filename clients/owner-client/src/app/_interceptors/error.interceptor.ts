import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { catchError, mergeMap } from "rxjs";
import { AccountService } from "../_services/account.service";

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastrService: ToastrService = inject(ToastrService);
  const accountService: AccountService = inject(AccountService);

  return next(req).pipe(
    catchError((error) => {
      if (error.status === 401) {
        if (error.error === "NVLD_SRNM")  {
          toastrService.info("Invalid username or password.");
          accountService.logout()
        } else {
          accountService.refreshToken()?.pipe(
            mergeMap(() => next(req))
          ).subscribe();
        }
      } else if (error.status === 400) {
        toastrService.error(error.error);
      } else if (error.status === 404) {
        toastrService.info("404 Error: Resource not found on the server. Please verify the request and try again");
      } else {
        toastrService.error("Something went wrong. Try again later.");
        console.log(error);
      }
      throw error;
    })
  );
}