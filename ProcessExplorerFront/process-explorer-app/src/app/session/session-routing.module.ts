import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SessionComponent } from './session.component';
import { SessionAllComponent } from './session-all/session-all.component';
import { SessionUserComponent } from './session-user/session-user.component';

const routes: Routes = [
    { 
        path: '', 
        component: SessionComponent,
        children: [
            { path: '', component: SessionAllComponent },
            { path: 'user', component: SessionUserComponent },
        ],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SessionRoutingModule {}
