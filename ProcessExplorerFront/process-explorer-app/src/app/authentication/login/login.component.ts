import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { FormValidationService } from 'src/app/shared/services/form-validation.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  public loginForm: FormGroup;

  constructor(public validation: FormValidationService) { }

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
    console.log(this.loginForm);
  }

  cleanForm(): void {
    this.loginForm.reset();
  }

}
