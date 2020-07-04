import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { ApplicationRoutingModule } from './application-routing.module';
import { ApplicationComponent } from './application.component';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        ReactiveFormsModule,
        ApplicationRoutingModule
    ],
    declarations: [
        ApplicationComponent,
    ]
})
export class ApplicationModule {}