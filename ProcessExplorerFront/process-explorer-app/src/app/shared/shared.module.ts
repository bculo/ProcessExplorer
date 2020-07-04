import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartsModule } from 'ng2-charts';
import { RefreshPageComponent } from './components/refresh-page/refresh-page.component';
import { BackComponent } from './components/back/back.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { ListPagesComponent } from './components/list-pages/list-pages.component';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { RouterModule } from '@angular/router';
import { TabItemComponent } from './components/tab-navigation/tab-item/tab-item.component';
import { PageTabNavigationComponent } from './components/tab-navigation/page-tab-navigation/page-tab-navigation.component';
import { ChartCanvasComponent } from './components/chart-canvas/chart-canvas.component';

@NgModule({
    declarations: [
        RefreshPageComponent,
        BackComponent,
        SpinnerComponent,
        SearchBarComponent,
        ListPagesComponent,
        ErrorMessageComponent,
        TabItemComponent,
        PageTabNavigationComponent,
        ChartCanvasComponent
    ],
    imports: [
        ChartsModule,
        CommonModule,
        RouterModule,
    ],
    exports: [
        CommonModule,
        ChartsModule,
        SpinnerComponent,
        BackComponent,
        SearchBarComponent,
        ListPagesComponent,
        RouterModule,
        TabItemComponent,
        PageTabNavigationComponent,
        ErrorMessageComponent,
        ChartCanvasComponent
    ]
})
export class SharedModule {}