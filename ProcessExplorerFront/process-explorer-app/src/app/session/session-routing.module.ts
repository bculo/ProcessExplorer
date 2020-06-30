import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SessionComponent } from './session.component';
import { SessionAllStatsComponent } from './statistic/session-all-stats/session-all-stats.component';
import { SessionUserStatsComponent } from './statistic/session-user-stats/session-user-stats.component';
import { SessionSearchComponent } from './session-search/session-search.component';
import { StatisticComponent } from './statistic/statistic.component';
import { DetailsComponent } from './details/details.component';
import { DetailsProcessesComponent } from './details/details-processes/details-processes.component';
import { DetailsApplicationsComponent } from './details/details-applications/details-applications.component';

const routes: Routes = [
    { 
        path: '', 
        component: SessionComponent,
        children: [
            { 
                path: '', 
                component: StatisticComponent,
                children: [ 
                    { path: '', component: SessionAllStatsComponent},
                    { path: 'user', component: SessionUserStatsComponent },
                ]
            },
            { path: 'search', component: SessionSearchComponent },
            { 
                path: 'details/:id',
                component: DetailsComponent,
                children: [ 
                    { path: '', component: DetailsApplicationsComponent },
                    { path: 'processes', component: DetailsProcessesComponent },
                ] 
            },
        ],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SessionRoutingModule {}
