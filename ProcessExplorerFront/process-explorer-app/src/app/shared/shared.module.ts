import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartsModule } from 'ng2-charts';
import { RefreshPageComponent } from './components/refresh-page/refresh-page.component';
import { BackComponent } from './components/back/back.component';

@NgModule({
    declarations: [
        RefreshPageComponent,
        BackComponent
    ],
    imports: [
        ChartsModule,
        CommonModule,
    ],
    exports: [
        CommonModule,
        ChartsModule,
    ]
})
export class SharedModule {}