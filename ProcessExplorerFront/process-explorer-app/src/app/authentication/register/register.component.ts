import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { FormValidationService } from 'src/app/shared/services/form-validation.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {
  public submited: boolean = false;
  public errorMessage: string | null = null;
  private userSub: Subscription;
  public registerForm: FormGroup;

  constructor(public validation: FormValidationService,
    private router: Router,
    private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.initForm();
    this.validation.setForm(this.registerForm);

    this.userSub = this.authService.user.subscribe((user) => {
      if(user) this.router.navigate(['/session']);
    })
  }

  initForm(): void {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', 
        [
          Validators.required, 
          Validators.minLength(environment.authentication.passwordLength)
        ])
    });
  }

  onSubmit(): void {
    if(!this.registerForm.valid) return;
    this.submited = true;
    this.errorMessage = null;
    this.authService.registerUser(this.registerForm.value)
      .subscribe(() => {
        this.submited = false;
        this.router.navigate(['/authentication'])
      },
      (error) => {
        this.errorMessage = error;
        this.submited = false;
      });
  }

  ngOnDestroy(): void {
    this.validation.removeForm();
    this.userSub.unsubscribe();
  }

  cleanForm(): void {
    this.registerForm.reset();
  }
}
