import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ApplicationUser } from '../shared/models/application-user.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginRequestModel, RegisterRequestModel, ILoginResponse } from './models/authentication.models';
import { environment } from 'src/environments/environment';
import { catchError, tap } from 'rxjs/operators'
import { FormValidationService } from '../shared/services/form-validation.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  public user = new BehaviorSubject<ApplicationUser>(null);

  constructor(private http: HttpClient,
    private router: Router,
    private validation: FormValidationService) { }

  public loginUser(user: LoginRequestModel) {
    return this.http.post(`${environment.api}/authentication/loginweb`, user)
      .pipe(catchError(this.validation.handleServerError));
  }

  public registerUser(user: RegisterRequestModel) {
    
  }
}
