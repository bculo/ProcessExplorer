import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { take, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ApplicationUser } from '../models/application-user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate  {

  constructor(private authService: AuthenticationService,
    private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> {
    return this.authService.user.pipe(take(1), map((user: ApplicationUser) => {
      if(user !== null) return true;
      return this.router.createUrlTree(['/authentication']);
    }));
  }

}
