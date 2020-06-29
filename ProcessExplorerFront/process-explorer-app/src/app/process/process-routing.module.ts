import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ProcessComponent } from './process.component';
import { SearchComponent } from './search/search.component';
import { SearchUserComponent } from './search/search-user/search-user.component';
import { SearchAllComponent } from './search/search-all/search-all.component';
import { StatisticComponent } from './statistic/statistic.component';
import { StatisticAllComponent } from './statistic/statistic-all/statistic-all.component';
import { StatisticUserComponent } from './statistic/statistic-user/statistic-user.component';

const routes: Routes = [
    { 
        path: '', 
        component: ProcessComponent,
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
export class ProcessRoutingModule {}