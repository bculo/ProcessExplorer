import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { ProcessComponent } from './process.component';
import { ProcessRoutingModule } from './process-routing.module';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        ReactiveFormsModule,
        ProcessRoutingModule
    ],
    declarations: [
        ProcessComponent
    ]
})
export class ProcessModule {}