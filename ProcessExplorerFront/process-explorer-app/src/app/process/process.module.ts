import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { ProcessComponent } from './process.component';
import { ProcessRoutingModule } from './process-routing.module';
import { SearchComponent } from './search/search.component';
import { StatisticComponent } from './statistic/statistic.component';
import { SearchUserComponent } from './search/search-user/search-user.component';
import { SearchAllComponent } from './search/search-all/search-all.component';
import { StatisticAllComponent } from './statistic/statistic-all/statistic-all.component';
import { StatisticUserComponent } from './statistic/statistic-user/statistic-user.component';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        ReactiveFormsModule,
        ProcessRoutingModule
    ],
    declarations: [
        ProcessComponent,
        SearchComponent,
        StatisticComponent,
        SearchUserComponent,
        SearchAllComponent,
        StatisticAllComponent,
        StatisticUserComponent,
    ]
})
export class ProcessModule {}