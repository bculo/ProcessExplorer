import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartsModule } from 'ng2-charts';

@NgModule({
    declarations: [

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