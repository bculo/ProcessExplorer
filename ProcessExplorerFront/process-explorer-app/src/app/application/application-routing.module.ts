import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ApplicationComponent } from './application.component';
import { AuthGuardService } from '../shared/services/auth-guard.service';
import { SearchComponent } from './search/search.component';
import { StatisticComponent } from './statistic/statistic.component';
import { StatisticAllComponent } from './statistic/statistic-all/statistic-all.component';
import { StatisticUserComponent } from './statistic/statistic-user/statistic-user.component';
import { SearchAllComponent } from './search/search-all/search-all.component';
import { SearchUserComponent } from './search/search-user/search-user.component';

const routes: Routes = [
    { 
        path: '', 
        component: ApplicationComponent,
        canActivate: [AuthGuardService],
        children: [
            { 
                path: 'search',
                component: SearchComponent,
                children: [
                    { path: '', component: SearchAllComponent },
                    { path: 'user', component: SearchUserComponent },
                ]
            },
            { 
                path: '',
                component: StatisticComponent,
                children: [
                    { path: '', component: StatisticAllComponent },
                    { path: 'user', component: StatisticUserComponent },
                ]
            }
        ],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ApplicationRoutingModule {}