import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { SessionRoutingModule } from './session-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SessionComponent } from './session.component';
import { SessionAllComponent } from './session-all/session-all.component';
import { SessionUserComponent } from './session-user/session-user.component';
import { SessionTabsComponent } from './session-tabs/session-tabs.component';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        SessionRoutingModule,
        ReactiveFormsModule
    ],
    declarations: [
        SessionComponent,
        SessionAllComponent,
        SessionUserComponent,
        SessionTabsComponent,
    ]
})
export class SessionModule {}