import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpParams } from '@angular/common/http';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { Observable } from 'rxjs';
import { take, exhaustMap } from 'rxjs/operators';



@Injectable()
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private authService: AuthenticationService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return this.authService.user.pipe(take(1), exhaustMap(user => {
      if(!user) return next.handle(req);
      const modifiedRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`
        }
      })
      return next.handle(modifiedRequest);
    }));
  }
}
