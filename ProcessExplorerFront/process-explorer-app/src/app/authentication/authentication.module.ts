import { NgModule } from '@angular/core';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        AuthenticationRoutingModule,
        ReactiveFormsModule
    ],
    declarations: [
        RegisterComponent,
        LoginComponent,
    ]
})
export class AuthenticationModule {}