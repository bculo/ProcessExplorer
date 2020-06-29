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
import { SearchTabsComponent } from './search/search-tabs/search-tabs.component';
import { SearchBarComponent } from './search/search-bar/search-bar.component';

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
        SearchTabsComponent,
        SearchBarComponent,
    ]
})
export class ProcessModule {}