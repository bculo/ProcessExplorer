import { Injectable } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { ApplicationUser } from '../shared/models/application-user.model';
import { HttpClient } from '@angular/common/http';
import { LoginRequestModel, RegisterRequestModel, ILoginResponse } from './models/authentication.models';
import { environment } from 'src/environments/environment';
import { catchError, tap, filter } from 'rxjs/operators'
import { FormValidationService } from '../shared/services/form-validation.service';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  public user = new BehaviorSubject<ApplicationUser>(null);



  constructor(private http: HttpClient,
    private validation: FormValidationService,
    private router: Router,
    private route: ActivatedRoute) { }

  public loginUser(user: LoginRequestModel) {
    user.isWebApp = true;
    return this.http.post(`${environment.api}/authentication/login`, user)
      .pipe(
        tap((response: ILoginResponse) => this.handleLoginLogic(response)),
        catchError(error => this.validation.handleServerError(error))
      );
  }

  public registerUser(user: RegisterRequestModel) {
    return this.http.post(`${environment.api}/authentication/register`, user)
      .pipe(
        catchError(error => this.validation.handleServerError(error))
      );
  }

  private handleLoginLogic(res: ILoginResponse) {
    var date = new Date(res.expireIn);
    const user = new ApplicationUser(res.userName, res.userId, res.jwtToken, date);
    localStorage.setItem('user', JSON.stringify(user));
    this.user.next(user);
  }

  public logout() {
    this.user.next(null);
    localStorage.removeItem('user');
  }

  public autoLogin() {
    const userAsString: string = localStorage.getItem('user');
    if(!userAsString) {
      return;
    }

    const tmp: {
      userName: string,
      userId: string,
      _token: string,
      _tokenExpirationDate: string
    } = JSON.parse(userAsString);

    const appuser = new ApplicationUser(tmp.userName, tmp.userId, tmp._token, new Date(tmp._tokenExpirationDate));

    if(!appuser.isValid()) {
      this.logout(); 
      this.router.navigate(['/authentication']);
    }

    this.user.next(appuser);
  }

}
