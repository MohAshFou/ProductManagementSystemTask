import { HttpClient, HttpHandler, HttpRequest, HttpEvent, HttpHandlerFn } from "@angular/common/http";
import { inject } from "@angular/core";


export function AuthInterceptor(req: HttpRequest<any>, next: HttpHandlerFn) {

  // if (token) {
  //        // we use clone to take copy from req because origin req We can't modify it.
  //   const clonedReq = req.clone({
  //     headers: req.headers.append('Authorization', `Bearer ${token}`)
  //   });
  //    console.log(clonedReq)
  //   return next(clonedReq);
  // }

  return next(req);
}
