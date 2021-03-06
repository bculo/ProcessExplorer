import { Component, OnInit, OnDestroy, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { FormValidationService } from 'src/app/shared/services/form-validation.service';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApplicationUser } from 'src/app/shared/models/application-user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  public loginForm: FormGroup;
  public submited: boolean = false;
  public errorMessage: string | null = null;
  private userSub: Subscription;

  constructor(public validation: FormValidationService,
    private router: Router,
    private authService: AuthenticationService) { }

  ngOnDestroy(): void {
    this.validation.removeForm();
    this.userSub.unsubscribe();
  }

  ngOnInit(): void {
    this.initForm();
    this.validation.setForm(this.loginForm);

    this.userSub = this.authService.user.subscribe((user) => {
      if(user) this.router.navigate(['/session']);
    })
  }

  initForm(): void {
    this.loginForm = new FormGroup({
      identifier: new FormControl('', Validators.required),
      password: new FormControl('', 
        [
          Validators.required, 
          Validators.minLength(environment.authentication.passwordLength)
        ])
    });
  }

  onSubmit(): void {
    if(!this.loginForm.valid) return;
    this.submited = true;
    this.errorMessage = null;
    this.authService.loginUser(this.loginForm.value)
      .subscribe(() => {
        this.submited = false;
        this.router.navigate(['/session']);
      },
      (error) => {
        this.errorMessage = error;
        this.submited = false;
      })
  }

  cleanForm(): void {
    this.loginForm.reset();
  }
}
