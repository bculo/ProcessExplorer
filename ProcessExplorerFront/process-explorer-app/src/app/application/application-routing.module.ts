import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ApplicationComponent } from './application.component';
import { AuthGuardService } from '../shared/services/auth-guard.service';

const routes: Routes = [
    { 
        path: '', 
        component: ApplicationComponent,
        canActivate: [AuthGuardService],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ApplicationRoutingModule {}