import { Component, OnInit, OnDestroy, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { FormValidationService } from 'src/app/shared/services/form-validation.service';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  public loginForm: FormGroup;
  public submited: boolean = false;
  public errorMessage: string | null = null;

  constructor(public validation: FormValidationService,
    private router: Router,
    private authService: AuthenticationService) { }

  ngOnDestroy(): void {
    this.validation.removeForm();
  }

  ngOnInit(): void {
    this.initForm();
    this.validation.setForm(this.loginForm);
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
        console.log("LOGED IN");
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
