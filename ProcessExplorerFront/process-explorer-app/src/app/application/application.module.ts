import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { ApplicationRoutingModule } from './application-routing.module';
import { ApplicationComponent } from './application.component';
import { StatisticComponent } from './statistic/statistic.component';
import { SearchComponent } from './search/search.component';
import { StatisticAllComponent } from './statistic/statistic-all/statistic-all.component';
import { StatisticUserComponent } from './statistic/statistic-user/statistic-user.component';
import { StatisticTabComponent } from './statistic/statistic-tab/statistic-tab.component';

@NgModule({
    imports: [
        RouterModule,
        SharedModule,
        ReactiveFormsModule,
        ApplicationRoutingModule
    ],
    declarations: [
        ApplicationComponent,
        StatisticComponent,
        SearchComponent,
        StatisticAllComponent,
        StatisticUserComponent,
        StatisticTabComponent,
    ]
})
export class ApplicationModule {}