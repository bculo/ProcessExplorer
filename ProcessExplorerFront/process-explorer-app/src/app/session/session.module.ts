import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { SessionRoutingModule } from './session-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SessionComponent } from './session.component';
import { SessionAllStatsComponent } from './statistic/session-all-stats/session-all-stats.component';
import { SessionUserStatsComponent } from './statistic/session-user-stats/session-user-stats.component';
import { SessionSearchComponent } from './session-search/session-search.component';
import { StatisticComponent } from './statistic/statistic.component';
import { DetailsComponent } from './details/details.component';
import { DetailsTabComponent } from './details/details-tab/details-tab.component';
import { DetailsProcessesComponent } from './details/details-processes/details-processes.component';
import { DetailsApplicationsComponent } from './details/details-applications/details-applications.component';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        SessionRoutingModule,
        ReactiveFormsModule
    ],
    declarations: [
        SessionComponent,
        SessionAllStatsComponent,
        SessionUserStatsComponent,
        SessionSearchComponent,
        StatisticComponent,
        DetailsComponent,
        DetailsTabComponent,
        DetailsProcessesComponent,
        DetailsApplicationsComponent,
    ]
})
export class SessionModule {}