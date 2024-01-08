import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { catchError } from "rxjs";

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastrService: ToastrService = inject(ToastrService);

  return next(req).pipe(
    catchError((error) => {
      switch (error.status) {
        case 401:
          toastrService.info(error.error);
          break;
        case 400:
          toastrService.error(error.error);
          break;
        case 404:
          toastrService.info("404 Error: Resource not found on the server. Please verify the request and try again");
          break;
        default:
          break;
      }
      throw error;
    })
  );
}