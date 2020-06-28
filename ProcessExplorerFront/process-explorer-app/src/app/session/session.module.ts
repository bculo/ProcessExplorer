import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { SessionRoutingModule } from './session-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SessionComponent } from './session.component';
import { OperatingSystemStatisticComponent } from './charts/operating-system-statistic/operating-system-statistic.component';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        SessionRoutingModule,
        ReactiveFormsModule
    ],
    declarations: [
        SessionComponent,
        OperatingSystemStatisticComponent,
    ]
})
export class SessionModule {}