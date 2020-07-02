import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        AdminRoutingModule,
    ],
    declarations: [
        AdminComponent,
    ]
})
export class AdminModule {}