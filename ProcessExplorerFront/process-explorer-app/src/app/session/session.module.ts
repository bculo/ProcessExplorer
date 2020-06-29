import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { SessionRoutingModule } from './session-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SessionComponent } from './session.component';
import { SessionAllStatsComponent } from './statistic/session-all-stats/session-all-stats.component';
import { SessionUserStatsComponent } from './statistic/session-user-stats/session-user-stats.component';
import { SessionTabsComponent } from './statistic/session-tabs/session-tabs.component';
import { SessionSearchComponent } from './session-search/session-search.component';
import { StatisticComponent } from './statistic/statistic.component';

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
        SessionTabsComponent,
        SessionSearchComponent,
        StatisticComponent,
    ]
})
export class SessionModule {}