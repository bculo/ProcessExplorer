import { IColumnChartDto } from 'src/app/shared/models/interfaces.models';

export interface IBestApplicationDay {
    day: Date;
    number: number;
}

export interface ITopApplications {
    chartRecords: IColumnChartDto
}
