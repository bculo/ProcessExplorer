import { NgModule } from '@angular/core';
import { AuthInterceptorService } from './shared/services/auth-interceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';


//Module for registering shared services like interceptors
@NgModule({
    providers: [
        {
          provide: HTTP_INTERCEPTORS,
          useClass: AuthInterceptorService,
          multi: true
        }
    ]
})
export class CoreModule {}